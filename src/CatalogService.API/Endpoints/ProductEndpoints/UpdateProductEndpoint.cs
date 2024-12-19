using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class UpdateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync(IProductUseCase productUseCase, ProductDTO productDTO, ObjectId id)
        {
            var result = await productUseCase.UpdateProductAsync(productDTO.MapToEntity(), id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
