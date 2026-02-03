using MediatR;
using Streamline.Domain.Enums;
using Streamline.Application.Results;

namespace Streamline.Application.Commands
{
    public class PayOrderByIdCommand : IRequest<OrderResult>
    {
        public int Id { get; set; }
    }
}