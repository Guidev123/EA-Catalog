using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Domain.Entities.Validations;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.UseCases.Product.Update
{
    public class UpdateProductHandler(IProductRepository productRepository) : Handler, IUseCase<UpdateProductRequest, ProductDTO>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<ProductDTO>> HandleAsync(UpdateProductRequest input)
        {
            var product = input.Product.MapToEntity();

            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            var oldProduct = await _productRepository.GetProductByIdAsync(input.Id);
            oldProduct.UpdateProduct(product);

            await _productRepository.UpdateProductAsync(oldProduct);
            return new(null, 204);
        }
    }
}
