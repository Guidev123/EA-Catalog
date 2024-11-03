using CatalogService.API.DTOs;
using CatalogService.Domain.Services;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class CreateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync).RequireAuthorization("Admin").Produces<IResult>();
        public static async Task<IResult> HandleAsync(IProductService productService, ProductDTO productDTO)
        {
            var product = ProductDTO.MapToEntity(productDTO);
            var result = await productService.CreateProductAsync(product);
            return result.IsSuccess ? TypedResults.Created() : TypedResults.BadRequest(result);
        }
    }
}
