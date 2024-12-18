using CatalogService.API.Middlewares;
using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync).Produces<List<Response<ProductDTO>>>();

        public static async Task<IResult> HandleAsync(IProductUseCase productUseCase,
                                                      [FromQuery] int pageSize = ApplicationMiddlewares.DEFAULT_PAGE_SIZE,
                                                      [FromQuery] int pageNumber = ApplicationMiddlewares.DEFAULT_PAGE_NUMBER)
        {
            var result = await productUseCase.GetAllProductsAsync(pageNumber, pageSize);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
