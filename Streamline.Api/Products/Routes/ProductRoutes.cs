using MediatR;
using AutoMapper;
using Streamline.API.Products.Dtos;
using Streamline.Application.Products.CreateProduct;

namespace Streamline.API.Products.Routes
{
    public static class ProductRoutes
    {
        public static void MapProductRoutes(this IEndpointRouteBuilder app)
        {
            var group = app
                .MapGroup("/product")
                .WithTags("Product");

            group.MapPost("/", async (
                CreateProductDto dto,
                IMediator mediator,
                IMapper mapper) =>
            {
                var command = mapper.Map<CreateProductCommand>(dto);
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .WithMetadata(new Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute(
                summary: "Create a new product",
                description: "Endpoint to create a product"
            ));
        }
    }
}
