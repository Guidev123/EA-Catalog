using CatalogService.API.DTOs;
using CatalogService.API.Mappers;
using CatalogService.API.Responses;
using CatalogService.API.UseCases.Interfaces;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class UpdateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync(IProductUseCase productUseCase, ProductDTO productDTO, string id)
        {
            var result = await productUseCase.UpdateProductAsync(productDTO.MapToEntity(), id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
