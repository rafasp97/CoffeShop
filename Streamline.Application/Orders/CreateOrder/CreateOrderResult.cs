using Streamline.Application.Orders.CreateOrderProduct;

namespace Streamline.Application.Orders.CreateOrder
{
    public class CreateOrderResult
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required List<CreateOrderProductResult> Products { get; set; }
        public decimal Total { get; set; }
        public required string Status { get; set; }
    }
}
