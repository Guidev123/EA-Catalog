using CatalogService.API.Helpers;
using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;

namespace CatalogService.API.Endpoints.Products
{
    public class CreateProductEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/", HandleAsync).Produces<Response<ProductDTO>>();
        public static async Task<IResult> HandleAsync(ProductDTO productDTO,
                                                      IUseCase<ProductDTO, string?> productUseCase)
        {
            var imageFile = Base64FileConverter.ConvertBase64ToIFormFile(productDTO.ImageBase64);
            if (imageFile.Data is null || !imageFile.IsSuccess) return TypedResults.BadRequest(imageFile);

            productDTO.Image = imageFile.Data;
            var result = await productUseCase.HandleAsync(productDTO);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data}", result)
                : TypedResults.BadRequest(result);
        }

    }
}
