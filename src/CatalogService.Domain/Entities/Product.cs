using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.Domain.Entities
{
    public class Product
    {
        public Product(string name, string description, decimal price, int quantityInStock)
        {
            Name = name;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;
            IsDeleted = false;
            CreatedAt = DateTime.Now;
        }

        [BsonId]
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string? ImageBlobId { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityInStock { get; private set; }
        public bool IsDeleted { get; private set; }
        public void SetProductAsDeleted() => IsDeleted = true;

        public void SetImageBlobId(string imageBlobId) => ImageBlobId = imageBlobId;    

        public void UpdateProduct(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            ImageBlobId = product.ImageBlobId;
            QuantityInStock = product.QuantityInStock;
        }

        // Cache Constructor
        public Product(ObjectId id, string name, string description, string image, decimal price, int quantityInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageBlobId = image;
            Price = price;
            QuantityInStock = quantityInStock;
            IsDeleted = false;
            CreatedAt = DateTime.Now;
        }
    }
}
