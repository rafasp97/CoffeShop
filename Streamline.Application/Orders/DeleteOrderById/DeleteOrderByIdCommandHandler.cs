using MediatR;
using Streamline.Application.Repositories;
using Streamline.Domain.Entities.Orders;
using Streamline.Domain.Entities.Products;
using Streamline.Application.Orders;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Streamline.Application.Orders.DeleteOrderById
{
    public class DeleteOrderByIdCommandHandler 
        : IRequestHandler<DeleteOrderByIdCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderByIdCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(
            DeleteOrderByIdCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.Id)
                ?? throw new InvalidOperationException("Order not found.");

            order.Delete();
            await _orderRepository.Update(order);

            return Unit.Value;
        }
    }
}
