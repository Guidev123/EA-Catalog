using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Domain.Entities;
using MongoDB.Bson;

namespace CatalogService.Application.UseCases.Interfaces
{
    public interface IProductUseCase
    {
        Task<Response<ProductDTO>> CreateProductAsync(ProductDTO productDTO);
        Task<Response<ProductDTO>> UpdateProductAsync(Product product, ObjectId id);
        Task<Response<ProductDTO>> DeleteProductAsync(ObjectId id);
        Task<Response<List<GetProductDTO>>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<Response<GetProductDTO>> GetProductByIdAsync(ObjectId id);
    }
}
