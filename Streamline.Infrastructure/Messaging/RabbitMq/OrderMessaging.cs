using Streamline.Domain.Enums;

namespace Streamline.Infrastructure.Messaging.RabbitMq;
public class OrderMessaging
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public EStatusOrder Status { get; set; }
}
