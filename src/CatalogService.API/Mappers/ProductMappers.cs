using CatalogService.API.DTOs;
using CatalogService.Domain.Entities;

namespace CatalogService.API.Mappers
{
    public static class ProductMappers
    {
        public static ProductDTO MapFromEntity(this ProductDTO dto) =>
            new(dto.Name, dto.Description, dto.Image, dto.Price, dto.QuantityInStock);
        public static Product MapToEntity(this ProductDTO dto) =>
            new(dto.Name, dto.Description, dto.Image, dto.Price, dto.QuantityInStock);

        public static ProductDTO MapFromEntity(Product entity) =>
            new(entity.Name, entity.Description, entity.Image, entity.Price, entity.QuantityInStock);
    }
}
