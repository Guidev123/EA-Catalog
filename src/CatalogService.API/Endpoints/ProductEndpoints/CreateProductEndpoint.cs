using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class CreateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync([FromForm] ProductDTO productDTO,
                                                      [FromServices] IProductUseCase productUseCase)
        {
            var result = await productUseCase.CreateProductAsync(productDTO);
            return result.IsSuccess ? TypedResults.Created() : TypedResults.BadRequest(result);
        }
    }
}
