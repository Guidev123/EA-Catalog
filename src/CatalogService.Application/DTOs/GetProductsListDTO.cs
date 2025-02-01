namespace CatalogService.Application.DTOs
{
    public class GetProductsListDTO
    {
        public GetProductsListDTO(Guid productId, string name, string description, string imageUrl, decimal price, int quantityInStock)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            QuantityInStock = quantityInStock;
        }

        public Guid ProductId { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageUrl { get; }
        public decimal Price { get; }
        public int QuantityInStock { get; }
    }
}
