
using CatalogService.API.DTOs;
using CatalogService.Domain.Responses;
using CatalogService.Domain.Services;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetProductByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync).Produces<IResult>();
        public static async Task<IResult> HandleAsync(IProductService productService, string id)
        {
            var result = await productService.GetProductByIdAsync(id);

            if (result.IsSuccess)
            {
                var product = ProductDTO.MapFromEntity(result.Data!);
                return TypedResults.Ok(new Response<ProductDTO>(product));
            }

            return TypedResults.BadRequest(result);
        }
    }
}
