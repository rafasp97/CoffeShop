using AutoMapper;
using Streamline.API.Products.Dtos;
using Streamline.Application.Products.CreateProduct;

namespace Streamline.API.Products.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, CreateProductCommand>();
        }
    }
}
