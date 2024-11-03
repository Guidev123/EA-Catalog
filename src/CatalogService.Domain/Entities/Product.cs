namespace CatalogService.Domain.Entities
{
    public class Product
    {
        public Product(string name, string description, string image, decimal price, int quantityInStock)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            Image = image;
            Price = price;
            QuantityInStock = quantityInStock;
            IsDeleted = false;
            CreatedAt = DateTime.Now;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public decimal Price { get; private set; }
        public int QuantityInStock { get; private set; }
        public bool IsDeleted { get; private set; }

        public void SetProductAsDeleted() => IsDeleted = true;
    }
}
