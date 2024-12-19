namespace CatalogService.Application.DTOs
{
    public class GetProductDTO
    {
        public GetProductDTO(string name, string description, string imageBlobId, decimal price, int quantityInStock)
        {
            Name = name;
            Description = description;
            ImageBlobId = imageBlobId;
            Price = price;
            QuantityInStock = quantityInStock;
        }

        public string Name { get; }
        public string Description { get; }
        public string ImageBlobId { get; }
        public decimal Price { get; }
        public int QuantityInStock { get; }
    }
}
