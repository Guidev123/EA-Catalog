using CatalogService.API.Application.DTOs;
using CatalogService.API.Application.Responses;
using CatalogService.API.Application.UseCases.Interfaces;
using CatalogService.API.Middlewares;
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
