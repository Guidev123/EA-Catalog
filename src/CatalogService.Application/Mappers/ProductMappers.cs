using CatalogService.Application.DTOs;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public static class ProductMappers
    {
        public static ProductDTO MapFromEntity(this ProductDTO dto) =>
            new(dto.Name, dto.Description, dto.ImageBase64, dto.Price, dto.QuantityInStock);
        public static Product MapToEntity(this ProductDTO dto) =>
            new(dto.Name, dto.Description, dto.Price, dto.QuantityInStock);

        public static GetProductDTO MapFromEntity(this Product entity) =>
            new(entity.Name, entity.Description, entity.ImageUrl!, entity.Price, entity.QuantityInStock);

        public static GetProductsListDTO MapProductListFromEntity(this Product entity) =>
            new(Guid.Parse(entity.Id), entity.Name, entity.Description, entity.ImageUrl!, entity.Price, entity.QuantityInStock);
    }
}
