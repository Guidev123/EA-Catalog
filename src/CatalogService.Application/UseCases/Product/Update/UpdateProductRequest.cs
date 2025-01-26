using CatalogService.Application.DTOs;
using MongoDB.Bson;

namespace CatalogService.Application.UseCases.Product.Update
{
    public class UpdateProductRequest
    {
        public UpdateProductRequest(Guid id, ProductDTO product)
        {
            Id = id;
            Product = product;
        }

        public Guid Id { get; set; }
        public ProductDTO Product { get; set; } = null!;
    }
}
