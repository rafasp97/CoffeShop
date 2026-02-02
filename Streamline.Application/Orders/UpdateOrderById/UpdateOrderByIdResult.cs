using Streamline.Application.Orders;

namespace Streamline.Application.Orders.UpdateOrderById
{
    public class UpdateOrderByIdResult
    {
        public int Id { get; set; }
        public required string Status { get; set; }
        public List<ProductResult> Products { get; set; } = new();
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
