using MediatR;
using Streamline.Domain.Enums;
using Streamline.Application.Orders.CreateOrderProduct;

namespace Streamline.Application.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResult>
    {
        public required int CustomerId { get; set; }
        public required List<CreateOrderProductCommand> Products { get; set; }
    }
}
