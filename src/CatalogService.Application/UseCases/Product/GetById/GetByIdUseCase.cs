using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.Storage;
using CatalogService.Domain.Repositories;
using MongoDB.Bson;

namespace CatalogService.Application.UseCases.Product.GetById
{
    public class GetByIdUseCase(ICacheService cacheService,
                                IProductRepository productRepository)
                              : UseCase<ObjectId, GetProductDTO>
    {
        private readonly ICacheService _cacheService = cacheService;
        private readonly IProductRepository _productRepository = productRepository;
        public override async Task<Response<GetProductDTO>> HandleAsync(ObjectId id)
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
    }
}
