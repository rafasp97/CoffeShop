using MediatR;
using Streamline.Application.Repositories;
using Streamline.Domain.Entities.Orders;
using Streamline.Domain.Entities.Products;
using Streamline.Application.Orders.CreateOrderProduct;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Streamline.Application.Orders.CreateOrder
{
    public class CreateOrderCommandHandler 
        : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<CreateOrderResult> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            var order = new Order(customer);

            foreach (var item in request.Products)
            {
                var product = await _productRepository.GetById(item.ProductId)
                    ?? throw new InvalidOperationException($"Product {item.ProductId} not found.");

                if (product.StockQuantity < item.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough stock for product '{product.Name}'."
                    );

                order.AddProduct(product, item.Quantity);
            }

            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();

            return new CreateOrderResult
            {
                Name = customer.Name,
                Email = customer.Contact!.Email,
                Phone = customer.Contact!.Phone,
                Status = order.Status.ToString(),
                Total = order.Total,
                Products = order.OrderProduct.Select(orderProduct => new CreateOrderProductResult
                {
                    Name = orderProduct.Product.Name,
                    UnitPrice = orderProduct.UnitPrice,
                    Quantity = orderProduct.Quantity,
                    Subtotal = orderProduct.Subtotal
                }).ToList()
            };
        }
    }
}
