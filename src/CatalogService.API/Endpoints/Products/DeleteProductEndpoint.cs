using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.Products
{
    public class DeleteProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapDelete("/{id}", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync([FromServices] IUseCase<Guid,
                                                      ProductDTO> productUseCase,
                                                      Guid id)
        {
            var result = await productUseCase.HandleAsync(id);
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
