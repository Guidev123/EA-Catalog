
using CatalogService.API.DTOs;
using CatalogService.API.Middlewares;
using CatalogService.Domain.Responses;
using CatalogService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.ProductEndpoints
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync).Produces<IResult>();

        public static async Task<IResult> HandleAsync([FromServices] IProductService productService,
                                                      [FromQuery] int pageSize = ApplicationMiddlewares.DEFAULT_PAGE_SIZE,
                                                      [FromQuery] int pageNumber = ApplicationMiddlewares.DEFAULT_PAGE_NUMBER)
        {
            var result = await productService.GetAllProductsAsync(pageSize, pageNumber);
            if (result.IsSuccess)
            {
                var product = result.Data?.Select(ProductDTO.MapFromEntity).ToList();
                return TypedResults.Ok(new Response<List<ProductDTO>>(product));
            }

            return TypedResults.BadRequest(result);
        }
    }
}
