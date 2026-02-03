using Streamline.Infrastructure.Persistence.SqlServer.Repositories;
using Streamline.Infrastructure.Persistence.MongoDb.Repositories;
using Streamline.Infrastructure.BackgroundWorkers.Workers;
using Streamline.Application.Interfaces.Queues;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Streamline.Infrastructure.Queues;
public class OrderProcessingQueue : BackgroundService, IOrderProcessingQueue
{
    private readonly Channel<int> _channel = Channel.CreateUnbounded<int>();
    private readonly IServiceScopeFactory _scopeFactory;

    public OrderProcessingQueue(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Enqueue(int orderId)
    {
        _channel.Writer.TryWrite(orderId);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var orderId in _channel.Reader.ReadAllAsync(stoppingToken))
        {

            using var scope = _scopeFactory.CreateScope();

            var _orderRepository = scope.ServiceProvider.GetRequiredService<OrderRepository>();
            var _logger = scope.ServiceProvider.GetRequiredService<LogRepository>();
            var _worker = scope.ServiceProvider.GetRequiredService<OrderProcessingWorker>();

            try{
                await _worker.Execute(orderId);
                await _logger.Low($"Payment process completed for OrderId = {orderId}.");
            }
            catch (Exception ex)
            {
                var order = await _orderRepository.GetById(orderId)
                    ?? throw new InvalidOperationException($"Order {orderId} not found.");

                order.Fail();
                await _orderRepository.Update(order);

                await _logger.Medium($"An error occurred while processing in a order. {orderId}: {ex.Message}");
            }
        }
    }
}
