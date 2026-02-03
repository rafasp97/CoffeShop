using AutoMapper;
using Streamline.API.Dtos;
using Streamline.Application.Commands;

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
