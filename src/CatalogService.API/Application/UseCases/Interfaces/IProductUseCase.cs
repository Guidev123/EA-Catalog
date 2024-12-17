using CatalogService.API.Application.DTOs;
using CatalogService.API.Application.Responses;
using CatalogService.Domain.Entities;

namespace CatalogService.API.Application.UseCases.Interfaces
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
