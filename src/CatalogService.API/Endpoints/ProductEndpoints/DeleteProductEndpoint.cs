using CatalogService.API.DTOs;
using CatalogService.API.Responses;
using CatalogService.API.UseCases.Interfaces;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class DeleteProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync(IProductUseCase productUseCase, string id)
        {
            var result = await productUseCase.DeleteProductAsync(id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
