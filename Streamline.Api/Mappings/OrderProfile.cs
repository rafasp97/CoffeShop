using AutoMapper;
using Streamline.API.Dtos;
using Streamline.Application.Commands;

namespace Streamline.API.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, CreateOrderCommand>();
            CreateMap<CreateOrderProductDto, CreateOrderProductCommand>();
            CreateMap<UpdateOrderDto, UpdateOrderByIdCommand>();
            CreateMap<CreateOrderProductDto, UpdateOrderProductCommand>(); 
        }
    }
}
