
using MediatR;
using AutoMapper;
using Streamline.API.Customers.Dtos;
using Streamline.Application.Customers.CreateCustomer;

namespace Streamline.API.Customers.Routes
{
    public static class CustomerRoutes
    {
        public static void MapCustomerRoutes(this IEndpointRouteBuilder app)
        {
            var group = app
                .MapGroup("/customer")
                .WithTags("Customer");

            group.MapPost("/", async (
                CreateCustomerDto dto,
                IMediator mediator,
                IMapper mapper) =>
            {
                var command = mapper.Map<CreateCustomerCommand>(dto);
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "Create a new customer",
                description: "Endpoint to create a customer"
            ));
        }
    }
}
