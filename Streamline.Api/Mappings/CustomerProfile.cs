using AutoMapper;
using Streamline.API.Dtos;
using Streamline.Application.Customers.CreateCustomer;

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
