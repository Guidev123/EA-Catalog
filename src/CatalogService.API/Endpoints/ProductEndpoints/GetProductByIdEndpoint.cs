
using CatalogService.API.Middlewares;
using CatalogService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetProductByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync).Produces<IResult>();
        public static async Task<IResult> HandleAsync([FromServices] IProductService productService, string id)
        {
            var result = await productService.GetProductByIdAsync(id);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
