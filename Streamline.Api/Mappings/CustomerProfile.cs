using AutoMapper;
using Streamline.API.Dtos;
using Streamline.Application.Commands;

namespace Streamline.API.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDto, CreateCustomerCommand>();
        }
    }
}
