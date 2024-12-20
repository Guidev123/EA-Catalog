namespace CatalogService.Application.DTOs
{
    public class GetProductDTO
    {
        public GetProductDTO(string name, string description, string imageUrl, decimal price, int quantityInStock)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            QuantityInStock = quantityInStock;
        }

        public string Name { get; }
        public string Description { get; }
        public string ImageUrl { get; }
        public decimal Price { get; }
        public int QuantityInStock { get; }
    }
}
