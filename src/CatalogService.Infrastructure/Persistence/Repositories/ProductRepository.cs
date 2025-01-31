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

        public async Task<Product> GetProductByIdAsync(Guid id) =>
            await _collection.Find(c => c.Id == id.ToString()).SingleOrDefaultAsync();

        public async Task CreateProductAsync(Product product) => await _collection.InsertOneAsync(product);

        public async Task UpdateProductAsync(Product product) => await _collection.ReplaceOneAsync(c => c.Id == product.Id, product);

        public async Task<List<Product>> GetProductsByIdsAsync(string ids)
        {
            var idsGuid = ids.Split(',')
                             .Select(id => Guid.TryParse(id, out var guid) ? guid : (Guid?)null)
                             .Where(guid => guid.HasValue)
                             .Select(guid => guid!.Value)
                             .ToList();

            if (!idsGuid.Any()) return new List<Product>();

            var filter = Builders<Product>.Filter.In(p => p.Id, idsGuid.Select(g => g.ToString()));
            filter = Builders<Product>.Filter.And(filter, Builders<Product>.Filter.Eq(p => p.IsDeleted, false));

            return await _collection.Find(filter).ToListAsync();
        }

    }
}
