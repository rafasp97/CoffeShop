namespace Streamline.Application.Interfaces.Queues;

public interface IOrderProcessingQueue
{
    void Enqueue(int orderId);
}
