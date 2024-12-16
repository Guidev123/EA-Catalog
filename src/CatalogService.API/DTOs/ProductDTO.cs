using CatalogService.Domain.Entities;

namespace CatalogService.API.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(string name, string description, string image, decimal price, int quantityInStock)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
            QuantityInStock = quantityInStock;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityInStock { get; private set; }
    }
}
