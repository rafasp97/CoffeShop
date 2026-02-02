using MediatR;

namespace Streamline.Application.Orders.DeleteOrderById
{
    public class DeleteOrderByIdCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
