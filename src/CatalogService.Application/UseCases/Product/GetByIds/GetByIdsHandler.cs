using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.UseCases.Product.GetByIds
{
    public class GetByIdsHandler(IProductRepository productRepository) : Handler, IUseCase<GetByIdsRequest, List<GetProductsListDTO>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<List<GetProductsListDTO>>> HandleAsync(GetByIdsRequest input)
        {
            var products = await _productRepository.GetProductsByIdsAsync(input.Ids);
            return products.Count == 0 || products is null
                ? new(null, 404, "Items not found")
                : new(products.Select(x => x.MapProductListFromEntity()).ToList(), 200, "Valid operation");
        }
    }
}
