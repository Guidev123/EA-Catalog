using CatalogService.API.DTOs;
using CatalogService.API.Mappers;
using CatalogService.API.Responses;
using CatalogService.API.Responses.Messages;
using CatalogService.API.UseCases.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Entities.Validations;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.CacheStorage;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.API.UseCases
{
    public class ProductUseCase(IProductRepository productRepository, ICacheService cacheService) : IProductUseCase
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Response<List<ProductDTO>>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var cacheKey = $"products_{pageNumber}_{pageSize}";
            var cacheProduct = await _cacheService.GetAsync<List<ProductDTO>>(cacheKey);
            if (cacheProduct is not null)
                return new Response<List<ProductDTO>>(cacheProduct, 200, ResponseMessages.VALID_OPERATION.GetDescription());

            var products = await _productRepository.GetAllProductsAsync(pageNumber, pageSize);
            if (products is null || products.Count == 0)
                return new Response<List<ProductDTO>>(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            await _cacheService.SetAsync(cacheKey, products);

            var productsResult = products.Select(ProductMappers.MapFromEntity).ToList();

            return new Response<List<ProductDTO>>(productsResult, 200, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> GetProductByIdAsync(string id)
        {
            var cacheProduct = await _cacheService.GetAsync<ProductDTO>(id);

            if (cacheProduct is not null)
                return new Response<ProductDTO>(cacheProduct);

            var product = await _productRepository.GetProductByIdAsync(id);
            var productResult = ProductMappers.MapFromEntity(product);
            if (product is null) return new Response<ProductDTO>(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            await _cacheService.SetAsync(id, product);

            return new Response<ProductDTO>(productResult, 200, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> CreateProductAsync(Product product)
        {
            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new Response<ProductDTO>(null, 400, ResponseMessages.VALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            await _productRepository.CreateProductAsync(product);
            return new Response<ProductDTO>(null, 201, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> DeleteProductAsync(string id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return new Response<ProductDTO>(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            product.SetProductAsDeleted();
            await _productRepository.UpdateProductAsync(product);
            return new Response<ProductDTO>(null, 204, ResponseMessages.VALID_OPERATION.GetDescription());
        }

        public async Task<Response<ProductDTO>> UpdateProductAsync(Product product, string id)
        {
            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new Response<ProductDTO>(null, 400, ResponseMessages.VALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            var oldProduct = await _productRepository.GetProductByIdAsync(id);
            oldProduct.UpdateProduct(product);

            await _productRepository.UpdateProductAsync(oldProduct);
            return new Response<ProductDTO>(null, 204);
        }

        protected ValidationResult ValidateEntity<TV, TE>(TV validation, TE entity) where TV
                : AbstractValidator<TE> where TE : class => validation.Validate(entity);
        private static string[] GetAllErrors(ValidationResult validationResult) =>
            validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
    }
}
