using MediatR;
using Streamline.Domain.Enums;
using Streamline.Application.Results;

namespace Streamline.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResult>
    {
        public int Id { get; set; }
    }
}
