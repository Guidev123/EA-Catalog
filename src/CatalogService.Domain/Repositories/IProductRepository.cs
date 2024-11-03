using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<Product> GetProductByIdAsync(string id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
