using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.Storage;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.UseCases.Product.GetAll
{
    public class GetAllUseCase(ICacheService cacheService,
                                      IProductRepository productRepository)
                                    : PagedUseCase<GetAllRequest, List<GetProductDTO>>
    {
        private readonly ICacheService _cacheService = cacheService;
        private readonly IProductRepository _productRepository = productRepository;

        public override async Task<PagedResponse<List<GetProductDTO>>> HandleAsync(GetAllRequest input)
        {
            var cacheKey = $"products_{input.PageNumber}_{input.PageSize}";
            var cacheProduct = await _cacheService.GetAsync<List<GetProductDTO>>(cacheKey);
            if (cacheProduct is not null)
                return new(cacheProduct.Count, cacheProduct, input.PageNumber, input.PageSize,
                           200, ResponseMessages.VALID_OPERATION.GetDescription());

            var products = await _productRepository.GetAllProductsAsync(input.PageNumber, input.PageSize);
            if (products is null || products.Count == 0)
                return new(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            await _cacheService.SetAsync(cacheKey, products);

            var productsResult = products.Select(ProductMappers.MapFromEntity).ToList();

            return new(products.Count, productsResult, input.PageNumber, input.PageSize,
                       200, ResponseMessages.VALID_OPERATION.GetDescription());
        }
    }
}
