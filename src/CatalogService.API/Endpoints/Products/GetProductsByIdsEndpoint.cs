using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Application.UseCases.Product.GetByIds;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Endpoints.Products
{
    public class GetProductsByIdsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/list/{ids}", HandleAsync).Produces<Response<List<GetProductDTO>>>();

        public static async Task<IResult> HandleAsync(string ids,
                                                      [FromServices] IUseCase<GetByIdsRequest, List<GetProductDTO>> useCase)
        {   
            var result = await useCase.HandleAsync(new(ids));
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
