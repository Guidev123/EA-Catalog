using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Application.UseCases.Product.Update;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.Products
{
    public class UpdateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPut("/{id}", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync([FromServices] IUseCase<UpdateProductRequest, ProductDTO> productUseCase,
                                                      ProductDTO productDTO,
                                                      Guid id)
        {
            var result = await productUseCase.HandleAsync(new(id, productDTO));
            return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
        }
    }
}
