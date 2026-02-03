using AutoMapper;
using Streamline.API.Dtos;
using Streamline.Application.Products.CreateProduct;

namespace Streamline.API.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, CreateProductCommand>();
        }
    }
}
