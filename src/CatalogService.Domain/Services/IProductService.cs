using CatalogService.Domain.Entities;
using CatalogService.Domain.Responses;

namespace CatalogService.Domain.Services
{
    public interface IProductService
    {
        Task<Response<Product>> CreateProductAsync(Product product);
        Task<Response<Product>> UpdateProductAsync(Product product, string id);
        Task<Response<Product>> DeleteProductAsync(string id);
        Task<Response<List<Product>>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<Response<Product>> GetProductByIdAsync(string id);
    }
}
