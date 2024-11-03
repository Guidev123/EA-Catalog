using CatalogService.API.Middlewares;
using CatalogService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync).Produces<IResult>();

        public static async Task<IResult> HandleAsync(IProductService productService,
                                                      [FromQuery] int pageSize = ApplicationMiddlewares.DEFAULT_PAGE_SIZE,
                                                      [FromQuery] int pageNumber = ApplicationMiddlewares.DEFAULT_PAGE_NUMBER)
        {
            var result = await productService.GetAllProductsAsync(pageNumber, pageSize);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
