using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.UseCases.Interfaces
{
    public interface IProductUseCase
    {
        Task<Response<ProductDTO>> CreateProductAsync(Product product);
        Task<Response<ProductDTO>> UpdateProductAsync(Product product, string id);
        Task<Response<ProductDTO>> DeleteProductAsync(string id);
        Task<Response<List<ProductDTO>>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<Response<ProductDTO>> GetProductByIdAsync(string id);
    }
}
