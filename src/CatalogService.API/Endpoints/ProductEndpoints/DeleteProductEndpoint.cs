using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class DeleteProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapDelete("/{id}", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync(IUseCase<ObjectId,
                                                      ProductDTO> productUseCase,
                                                      ObjectId id)
        {
            var result = await productUseCase.HandleAsync(id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
