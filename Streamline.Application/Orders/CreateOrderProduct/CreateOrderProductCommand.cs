using MediatR;
using Streamline.Domain.Enums;

namespace Streamline.Application.Orders.CreateOrderProduct
{
    public class CreateOrderProductCommand
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
