namespace CatalogService.Application.DTOs
{
    public record GetProductDTO(string Name, string Description, string ImageBlobId, decimal Price, int QuantityInStock);
}
