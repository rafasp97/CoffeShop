using MediatR;
using Streamline.Domain.Enums;

namespace Streamline.Application.Orders.UpdateOrderById
{
    public class UpdateOrderByIdCommand : IRequest<UpdateOrderByIdResult>
    {
        public required int OrderId { get; set; }
        public required List<UpdateOrderProductCommand> Products { get; set; }
    }

    public class UpdateOrderProductCommand
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}