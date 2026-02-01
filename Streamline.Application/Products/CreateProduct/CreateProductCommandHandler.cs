using Streamline.Application.Repositories;
using Streamline.Domain.Entities.Products;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Streamline.Application.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var product = new Product(
                request.Name,
                request.Price,
                request.StockQuantity,
                request.Active,
                request.Description
            );

            _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();

            return new CreateProductResult
            {
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Active = product.Active,
                Description = product.Description
            };
        }
    }
}
