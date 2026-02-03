using Streamline.Infrastructure.Persistence.SqlServer.Repositories;
using Streamline.Infrastructure.Messaging.RabbitMq;
using Streamline.Domain.Entities.Orders;

namespace Streamline.Infrastructure.BackgroundWorkers.Workers;
public class OrderProcessingWorker
{
    private readonly OrderRepository _orderRepository;
    private readonly RabbitMqPublisher _publisher;

    public OrderProcessingWorker(OrderRepository orderRepository, RabbitMqPublisher publisher)
    {
        _orderRepository = orderRepository;
        _publisher = publisher;
    }

    public async Task Execute(int orderId)
    {
        var order = await _orderRepository.GetById(orderId)
            ?? throw new InvalidOperationException($"Order {orderId} not found.");

        //TODO: implementar método de pagamento e retirar o delay.
        await Task.Delay(TimeSpan.FromSeconds(5));

        order.Pay();
        await _orderRepository.Update(order);

        //TODO: implementar o processamento de dados (faturamento, etc..) e retirar o delay.
        await Task.Delay(TimeSpan.FromSeconds(10));

        order.Process();
        await _orderRepository.Update(order);

        var message = new OrderMessaging 
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            Status = order.Status
        };

        await _publisher.ProcessedOrder(message);
    }
}
