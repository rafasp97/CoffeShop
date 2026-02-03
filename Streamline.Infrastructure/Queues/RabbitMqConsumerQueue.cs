using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Streamline.Infrastructure.Messaging.RabbitMq;

namespace Streamline.Infrastructure.Queues;
public class RabbitMqConsumerQueue : BackgroundService
{
    private readonly RabbitMqConsumer _consumer;

    public RabbitMqConsumerQueue(RabbitMqConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Start();
        return Task.CompletedTask;
    }
}

