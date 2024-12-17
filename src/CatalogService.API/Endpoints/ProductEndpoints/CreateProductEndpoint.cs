using CatalogService.API.Application.DTOs;
using CatalogService.API.Application.Mappers;
using CatalogService.API.Application.Responses;
using CatalogService.API.Application.UseCases.Interfaces;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class CreateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync(IProductUseCase productUseCase, ProductDTO productDTO)
        {
            var result = await productUseCase.CreateProductAsync(productDTO.MapToEntity());
            return result.IsSuccess ? TypedResults.Created() : TypedResults.BadRequest(result);
        }
    }
}
