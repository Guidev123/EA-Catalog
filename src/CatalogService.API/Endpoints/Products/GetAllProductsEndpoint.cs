using CatalogService.Application;
using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Application.UseCases.Product.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.Products
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/", HandleAsync).Produces<List<Response<ProductDTO>>>();

        public static async Task<IResult> HandleAsync([FromServices] IPagedUseCase<GetAllProductsRequest, List<GetProductDTO>> productUseCase,
                                                      [FromQuery] int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE,
                                                      [FromQuery] int pageNumber = ApplicationModule.DEFAULT_PAGE_NUMBER)
        {
            var result = await productUseCase.HandleAsync(new(pageNumber, pageSize));
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
