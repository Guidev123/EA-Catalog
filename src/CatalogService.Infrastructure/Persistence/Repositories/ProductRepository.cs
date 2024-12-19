using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(IMongoDatabase mongoDatabase) : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection = mongoDatabase.GetCollection<Product>("products");

        public async Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize) =>
            await _collection.Find(x => !x.IsDeleted).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();

        public async Task<Product> GetProductByIdAsync(ObjectId id) => await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();

        public async Task CreateProductAsync(Product product) => await _collection.InsertOneAsync(product);

        public async Task UpdateProductAsync(Product product) => await _collection.ReplaceOneAsync(c => c.Id == product.Id, product);
    }
}
