using CatalogService.Domain.Entities;

namespace CatalogService.API.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(string id, string name, string description, string image, DateTime createdAt, decimal price, int quantityInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            CreatedAt = createdAt;
            Price = price;
            QuantityInStock = quantityInStock;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityInStock { get; private set; }

        public static Product MapToEntity(ProductDTO dto) =>
            new(dto.Id, dto.Name, dto.Description, dto.Image, dto.Price, dto.QuantityInStock);

        public static ProductDTO MapFromEntity(Product entity) =>
            new(entity.Id, entity.Name, entity.Description, entity.Image, entity.CreatedAt, entity.Price, entity.QuantityInStock);
    }
}
