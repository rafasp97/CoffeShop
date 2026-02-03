using MediatR;
using Streamline.Domain.Enums;
using Streamline.Application.Results;

namespace Streamline.Application.Queries
{
    public class ListOrderQuery : IRequest<ListOrderResult>
    {
        public EStatusOrder? Status { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
