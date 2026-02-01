using MediatR;
using AutoMapper;
using Streamline.API.Orders.Dtos;
using Streamline.Application.Orders.CreateOrder;

namespace Streamline.API.Orders.Routes
{
    public static class OrderRoutes
    {
        public static void MapOrderRoutes(this IEndpointRouteBuilder app)
        {
            var group = app
                .MapGroup("/order")
                .WithTags("Order");

            group.MapPost("/", async (
                CreateOrderDto dto,
                IMediator mediator,
                IMapper mapper) =>
            {
                var command = mapper.Map<CreateOrderCommand>(dto);
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "Create a new order",
                description: "Endpoint to create an order"
            ));
        }
    }
}
