using CatalogService.Application.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Application.UseCases
{
    public interface IUseCase<I, O>
    {
        Task<Response<O>> HandleAsync(I input);
        string[] GetAllErrors(ValidationResult validationResult);
        ValidationResult ValidateEntity<TV, TE>(TV validation, TE entity) where TV
                : AbstractValidator<TE> where TE : class;
    }
}
