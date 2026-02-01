using AutoMapper;
using Streamline.API.Orders.Dtos;
using Streamline.Application.Orders.CreateOrder;
using Streamline.Application.Orders.CreateOrderProduct;

namespace Streamline.API.Orders.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, CreateOrderCommand>();
            
            CreateMap<CreateOrderProductDto, CreateOrderProductCommand>();
        }
    }
}
