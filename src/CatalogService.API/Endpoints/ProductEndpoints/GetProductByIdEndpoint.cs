using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetProductByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync).Produces<Response<GetProductDTO>>();
        public static async Task<IResult> HandleAsync(IProductUseCase productUseCase, ObjectId id)
        {
            var result = await productUseCase.GetProductByIdAsync(id);

            if (result.IsSuccess && result.Data is not null)
                return TypedResults.Ok(new Response<GetProductDTO>(result.Data));

            return TypedResults.BadRequest(result);
        }
    }
}
