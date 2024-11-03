using CatalogService.API.DTOs;
using CatalogService.Domain.Services;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class UpdateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync).RequireAuthorization("ADM").Produces<IResult>();
        public static async Task<IResult> HandleAsync(IProductService productService, ProductDTO productDTO, string id)
        {
            var product = ProductDTO.MapToEntity(productDTO);
            var result = await productService.UpdateProductAsync(product, id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
