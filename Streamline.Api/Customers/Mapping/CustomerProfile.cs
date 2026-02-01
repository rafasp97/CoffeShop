using AutoMapper;
using Streamline.API.Customers.Dtos;
using Streamline.Application.Customers.CreateCustomer;

namespace Streamline.API.Customers.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDto, CreateCustomerCommand>();
        }
    }
}
