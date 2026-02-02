using MediatR;
using AutoMapper;
using Streamline.API.Orders.Dtos;
using Streamline.Application.Orders.CreateOrder;
using Streamline.Application.Orders.ListOrder;
using Streamline.Application.Orders.GetOrderById;
using Streamline.Application.Orders.DeleteOrderById;
using Streamline.Application.Orders.UpdateOrderById;
using Streamline.Domain.Enums;

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

            group.MapGet("/", async (
                EStatusOrder? status,
                int? customerId,
                DateTime? createdFrom,
                DateTime? createdTo,
                IMediator mediator) =>
            {
                var query = new ListOrderQuery
                {
                    Status = status,
                    CustomerId = customerId,
                    CreatedFrom = createdFrom,
                    CreatedTo = createdTo
                };

                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "List orders",
                description: "List orders filtered by status, customer and creation date"
            ));

            group.MapGet("/{id}", async (int id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetOrderByIdQuery { Id = id });
                return Results.Ok(result);
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "Get order by ID",
                description: "Retrieves a single order along with its customer and product details based on the specified order ID."
            ));

            group.MapDelete("/{id}", async (int id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteOrderByIdCommand { Id = id });
                return Results.NoContent();
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "Delete order by ID",
                description: "Deletes a specific order based on the provided order ID. Once deleted, the order cannot be recovered."
            ));

            group.MapPut("update-order/{id}", async (
                int id,
                UpdateOrderDto dto,
                IMediator mediator,
                IMapper mapper) =>
            {
                var command = mapper.Map<UpdateOrderByIdCommand>(dto);
                command.OrderId = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "Update an existing order",
                description: "Updates the details of an existing order, including products and quantities based on the provided order ID."
            ));

        }
    }
}
