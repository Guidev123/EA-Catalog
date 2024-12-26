using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.Storage;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Domain.Entities.Validations;
using CatalogService.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.UseCases.Product.Create
{
    public class CreateProductHandler(IProductRepository productRepository, IBlobService blob) : Handler, IUseCase<ProductDTO, ProductDTO>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IBlobService _blob = blob;
        public async Task<Response<ProductDTO>> HandleAsync(ProductDTO input)
        {
            var product = input.MapToEntity();

            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            product.SetImageBloUrl(await UploadImage(input.Image!));
            await _productRepository.CreateProductAsync(product);

            return new(null, 201, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        private async Task<string> UploadImage(IFormFile formFile)
        {
            using Stream stream = formFile.OpenReadStream();

            var filedId = await _blob.UploadAsync(stream, formFile.ContentType);

            return filedId.ToString();
        }
    }
}
