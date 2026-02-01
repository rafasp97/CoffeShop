namespace Streamline.Application.Orders.CreateOrderProduct
{
    public class CreateOrderProductResult
    {
        public required string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
