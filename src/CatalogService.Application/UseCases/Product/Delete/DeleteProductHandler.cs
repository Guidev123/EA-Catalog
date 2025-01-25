using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Domain.Repositories;
using MongoDB.Bson;

namespace CatalogService.Application.UseCases.Product.Delete
{
    public class DeleteProductHandler(IProductRepository productRepository) : Handler, IUseCase<ObjectId, ProductDTO>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<ProductDTO>> HandleAsync(ObjectId id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return new(false, 404, null, ResponseMessages.INVALID_OPERATION.GetDescription());

            product.SetProductAsDeleted();
            await _productRepository.UpdateProductAsync(product);
            return new(false, 204, null, ResponseMessages.VALID_OPERATION.GetDescription());
        }
    }
}
