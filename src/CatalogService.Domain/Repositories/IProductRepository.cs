using CatalogService.Domain.Entities;
using MongoDB.Bson;

namespace CatalogService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<List<Product>> GetProductsByIdsAsync(string ids);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
