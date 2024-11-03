using CatalogService.Domain.Entities;
using CatalogService.Domain.Entities.Validations;
using CatalogService.Domain.Repositories;
using CatalogService.Domain.Responses;
using CatalogService.Domain.Services;
using CatalogService.Infrastructure.CacheStorage;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.API.Services
{
    public class ProductService(IProductRepository productRepository, ICacheService cacheService) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Response<List<Product>>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var cacheKey = $"products_{pageNumber}_{pageSize}";
            var cacheProduct = await _cacheService.GetAsync<List<Product>>(cacheKey);
            if (cacheProduct is null)
            {
                var products = await _productRepository.GetAllProductsAsync(pageSize, pageNumber);
                if (products is null || products.Count == 0) return new Response<List<Product>>(cacheProduct, 404);
                await _cacheService.SetAsync(cacheKey, products);

                return new Response<List<Product>>(products, 200);
            }

            return new Response<List<Product>>(cacheProduct, 200);
        }

        public async Task<Response<Product>> GetProductByIdAsync(string id)
        {
            var cacheProduct = await _cacheService.GetAsync<Product>(id);
            if (cacheProduct is null)
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product is null) return new Response<Product>(null, 404);
                await _cacheService.SetAsync(id, product);
            }

            return new Response<Product>(cacheProduct);
        }

        public async Task<Response<Product>> CreateProductAsync(Product product)
        {
            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new Response<Product>(null, 400, errors);
            }

            await _productRepository.CreateProductAsync(product);
            return new Response<Product>(null, 201);
        }

        public async Task<Response<Product>> DeleteProductAsync(string id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return new Response<Product>(null, 404);

            product.SetProductAsDeleted();
            await _productRepository.UpdateProductAsync(product);
            return new Response<Product>(null, 204);
        }


        public Task<Response<Product>> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        protected ValidationResult ValidateEntity<TV, TE>(TV validation, TE entity) where TV
                : AbstractValidator<TE> where TE : class => validation.Validate(entity);
    }
}
