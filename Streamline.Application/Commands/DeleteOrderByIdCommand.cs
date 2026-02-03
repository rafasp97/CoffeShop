using MediatR;

namespace Streamline.Application.Commands
{
    public class DeleteOrderByIdCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
    }
}
