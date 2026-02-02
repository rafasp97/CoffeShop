using MediatR;
using Streamline.Application.Repositories;
using Streamline.Domain.Entities.Orders;
using Streamline.Domain.Entities.Products;
using Streamline.Domain.Enums;
using Streamline.Application.Orders;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Streamline.Application.Orders.UpdateOrderById
{
    public class UpdateOrderByIdCommandHandler 
        : IRequestHandler<UpdateOrderByIdCommand, UpdateOrderByIdResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public UpdateOrderByIdCommandHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<UpdateOrderByIdResult> Handle(
            UpdateOrderByIdCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId)
                ?? throw new InvalidOperationException("Order not found.");

            order.CheckIfCanUpdateProducts();

            foreach (var item in request.Products)
            {
                var product = await _productRepository.GetById(item.ProductId)
                    ?? throw new InvalidOperationException($"Product {item.ProductId} not found.");

                product.EnsureSufficientStock(item.Quantity);

                var orderProduct = order.OrderProduct
                    .FirstOrDefault(op => op.Product.Id == item.ProductId);
                
                handleUpdateOrderProducts(item, order, product);
            }

            RemoveProductsNotInRequest(order, request);

            await _orderRepository.Update(order);

            return new UpdateOrderByIdResult
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                Total = order.Total,
                CreatedAt = order.CreatedAt,
                Products = order.OrderProduct
                    .Where(orderProduct => orderProduct.DeletedAt == null)
                    .Select(orderProduct => new ProductResult
                    {
                        Name = orderProduct.Product.Name,
                        UnitPrice = orderProduct.UnitPrice,
                        Quantity = orderProduct.Quantity,
                        Subtotal = orderProduct.Subtotal
                    })
                    .ToList()
            };
        }

        public void handleUpdateOrderProducts(UpdateOrderProductCommand item, Order order, Product product) {
            var orderProduct = order.OrderProduct
                .FirstOrDefault(op => op.Product.Id == item.ProductId);

            if (orderProduct == null || (item.Quantity > orderProduct.Quantity)){
                order.AddProduct(product, item.Quantity);
            } else {
                order.RemoveProduct(product, item.Quantity);
            }
        }

        public void RemoveProductsNotInRequest(Order order, UpdateOrderByIdCommand request) {
            var requestProductIds = request.Products
                .Select(p => p.ProductId)
                .ToHashSet();

            var orderProductsToRemove = order.OrderProduct
                .Where(op => op.DeletedAt == null && !requestProductIds.Contains(op.Product.Id))
                .ToList();

            foreach (var orderProduct in orderProductsToRemove)
            {
                order.RemoveProduct(orderProduct.Product, 0);
            }
        }
    }
}
