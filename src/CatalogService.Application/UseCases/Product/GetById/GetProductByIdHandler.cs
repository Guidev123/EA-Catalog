using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.Storage;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Domain.Repositories;
using MongoDB.Bson;

namespace CatalogService.Application.UseCases.Product.GetById
{
    public class GetProductByIdHandler(ICacheService cacheService,
                                IProductRepository productRepository)
                              : Handler, IUseCase<ObjectId, GetProductDTO>
    {
        private readonly ICacheService _cacheService = cacheService;
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<GetProductDTO>> HandleAsync(ObjectId id)
        {
            var cacheProduct = await _cacheService.GetAsync<GetProductDTO>(id.ToString());

            if (cacheProduct is not null)
                return new(true, 200, cacheProduct);

            var product = await _productRepository.GetProductByIdAsync(id);
            var productResult = ProductMappers.MapFromEntity(product);
            if (product is null) return new(false, 404, null, ResponseMessages.INVALID_OPERATION.GetDescription());

            await _cacheService.SetAsync(id.ToString(), product);

            return new(true, 200,productResult, ResponseMessages.VALID_OPERATION.GetDescription());
        }
    }
}
