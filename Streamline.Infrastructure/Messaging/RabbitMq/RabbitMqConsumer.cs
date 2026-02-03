using Streamline.Domain.Entities.Orders;       
using Streamline.Domain.Enums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Streamline.Infrastructure.Persistence.SqlServer.Repositories;

namespace Streamline.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqConsumer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RabbitMqPublisher _publisher;
        private readonly IModel _channel;

        public RabbitMqConsumer(IServiceScopeFactory scopeFactory, RabbitMqPublisher publisher, IConnection connection)
        {
            _scopeFactory = scopeFactory;
            _publisher = publisher;
            _channel = connection.CreateModel();
        }

        public void Start()
        {
            ConsumeQueue<OrderMessaging>("processed-orders", HandleProcessedOrder);
            ConsumeQueue<OrderMessaging>("shipped-orders", HandleShippedOrder);
        }

        private void ConsumeQueue<T>(string queueName, Func<T, Task> handler)
        {
            _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<T>(body);

                if(message == null) 
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                    return;
                }

                try {
                    await handler(message);
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch {
                    //Se algo falhar, retira da fila. Mas isso faz com que pedidos sejam perdidos.
                    //TODO: NECESSÁRIO IMPLEMENTAR RESILIÊNCIA DOS DADOS.
                   _channel.BasicNack(ea.DeliveryTag, false, false);
                }
            };

            _channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
        }


        private async Task HandleProcessedOrder(OrderMessaging message)
        {
            using var scope = _scopeFactory.CreateScope();
            var _orderRepository = scope.ServiceProvider.GetRequiredService<OrderRepository>();

            var order = await _orderRepository.GetById(message.OrderId)
                ?? throw new InvalidOperationException($"Order {message.OrderId} not found.");

            //Simula tempo de envio do pedido ao cliente...
            await Task.Delay(TimeSpan.FromSeconds(5));

            order.Ship();
            await _orderRepository.Update(order);

            await _publisher.ShippedOrder(new OrderMessaging
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status
            });
        }

        private async Task HandleShippedOrder(OrderMessaging message)
        {
            using var scope = _scopeFactory.CreateScope();
            var _orderRepository = scope.ServiceProvider.GetRequiredService<OrderRepository>();

            var order = await _orderRepository.GetById(message.OrderId)
                ?? throw new InvalidOperationException($"Order {message.OrderId} not found.");

            //Simula tempo de completar (ex: confirmar entrega)...
            await Task.Delay(TimeSpan.FromSeconds(5));

            order.Completed();
            await _orderRepository.Update(order);

        }

    }
}
