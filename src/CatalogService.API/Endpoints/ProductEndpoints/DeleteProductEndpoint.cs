using CatalogService.Domain.Services;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class DeleteProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync).RequireAuthorization("ADM").Produces<IResult>();
        public static async Task<IResult> HandleAsync(IProductService productService, string id)
        {
            var result = await productService.DeleteProductAsync(id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
