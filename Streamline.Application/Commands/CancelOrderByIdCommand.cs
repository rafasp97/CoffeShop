using MediatR;
using Streamline.Domain.Enums;
using Streamline.Application.Results;

namespace Streamline.Application.Commands
{
    public class CancelOrderByIdCommand : IRequest<OrderResult>
    {
        public int Id { get; set; }
    }
}