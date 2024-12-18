namespace CatalogService.Application.DTOs
{
    public record ProductDTO(string Name, string Description, string Image, decimal Price, int QuantityInStock)
    {
        public string Name { get; private set; } = Name;
        public string Description { get; private set; } = Description;
        public string Image { get; private set; } = Image;
        public decimal Price { get; private set; } = Price;
        public int QuantityInStock { get; private set; } = QuantityInStock;
    }
}
