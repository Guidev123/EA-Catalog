using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetProductByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/{id}", HandleAsync).Produces<Response<GetProductDTO>>();
        public static async Task<IResult> HandleAsync(IUseCase<ObjectId, GetProductDTO> productUseCase,
                                                      ObjectId id)
        {
            var result = await productUseCase.HandleAsync(id);

            if (result.IsSuccess && result.Data is not null)
                return TypedResults.Ok(new Response<GetProductDTO>(result.Data));

            return TypedResults.BadRequest(result);
        }
    }
}
