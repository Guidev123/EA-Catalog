using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CatalogService.Application.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(string name, string description, string imageBase64, decimal price, int quantityInStock)
        {
            Name = name;
            Description = description;
            ImageBase64 = imageBase64;
            Price = price;
            QuantityInStock = quantityInStock;
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageBase64 { get; set; } = string.Empty; 
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        [JsonIgnore]
        public IFormFile? Image { get; set; }
    }
}
