using CatalogService.Application.DTOs;
using CatalogService.Application.Mappers;
using CatalogService.Application.Responses;
using CatalogService.Application.Responses.Messages;
using CatalogService.Domain.Entities.Validations;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.UseCases.Product.Update
{
    public class UpdateUseCase(IProductRepository productRepository) : UseCase<UpdateRequest, ProductDTO>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public override async Task<Response<ProductDTO>> HandleAsync(UpdateRequest input)
        {
            var product = input.Product.MapToEntity();

            var validationResult = ValidateEntity(new ProductValidation(), product);

            if (!validationResult.IsValid)
                return new(null, 400, ResponseMessages.VALID_OPERATION.GetDescription(), GetAllErrors(validationResult));

            var oldProduct = await _productRepository.GetProductByIdAsync(input.Id);
            oldProduct.UpdateProduct(product);

            await _productRepository.UpdateProductAsync(oldProduct);
            return new(null, 204);
        }
    }
}
