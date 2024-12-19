using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CatalogService.Application.DTOs
{
    public record ProductDTO(string Name, string Description, IFormFile Image, decimal Price, int QuantityInStock);
}
