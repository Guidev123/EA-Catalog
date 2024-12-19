using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.Storage;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Entities.Validations;
using CatalogService.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace CatalogService.Application.UseCases
{
    public class ProductUseCase(IProductRepository productRepository,
                                ICacheService cacheService,
                                IBlobService blob) : IProductUseCase
    {
        private readonly IBlobService _blob = blob;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Response<List<GetProductDTO>>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var cacheKey = $"products_{pageNumber}_{pageSize}";
            var cacheProduct = await _cacheService.GetAsync<List<GetProductDTO>>(cacheKey);
            if (cacheProduct is not null)
                return new(cacheProduct, 200, ResponseMessages.VALID_OPERATION.GetDescription());

            var products = await _productRepository.GetAllProductsAsync(pageNumber, pageSize);
            if (products is null || products.Count == 0)
                return new(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            await _cacheService.SetAsync(cacheKey, products);

            var productsResult = products.Select(ProductMappers.MapFromEntity).ToList();

            return new(productsResult, 200, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<GetProductDTO>> GetProductByIdAsync(ObjectId id)
        {
            var cacheProduct = await _cacheService.GetAsync<GetProductDTO>(id.ToString());

            if (cacheProduct is not null)
                return new(cacheProduct);

            var product = await _productRepository.GetProductByIdAsync(id);
            var productResult = ProductMappers.MapFromEntity(product);
            if (product is null) return new(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            await _cacheService.SetAsync(id.ToString(), product);

            return new(productResult, 200, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> CreateProductAsync(ProductDTO productDTO)
        {
            var product = productDTO.MapToEntity();

            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            product.SetImageBlobId(await UploadImage(productDTO.Image));
            await _productRepository.CreateProductAsync(product);

            return new(null, 201, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> DeleteProductAsync(ObjectId id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return new(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            product.SetProductAsDeleted();
            await _productRepository.UpdateProductAsync(product);
            return new(null, 204, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> UpdateProductAsync(Product product, ObjectId id)
        {
            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new(null, 400, ResponseMessages.VALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            var oldProduct = await _productRepository.GetProductByIdAsync(id);
            oldProduct.UpdateProduct(product);

            await _productRepository.UpdateProductAsync(oldProduct);
            return new(null, 204);
        }

        private async Task<string> UploadImage(IFormFile formFile)
        {
            using Stream stream = formFile.OpenReadStream();

            var filedId = await _blob.UploadAsync(stream, formFile.ContentType);

            return filedId.ToString();
        }

        private static ValidationResult ValidateEntity<TV, TE>(TV validation, TE entity) where TV
                : AbstractValidator<TE> where TE : class => validation.Validate(entity);
        private static string[] GetAllErrors(ValidationResult validationResult) =>
            validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
    }
}
