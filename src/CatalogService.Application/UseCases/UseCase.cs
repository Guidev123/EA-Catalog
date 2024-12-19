using CatalogService.Application.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Application.UseCases
{
    public class UseCase<I, O> : IUseCase<I, O>
    {
        public string[] GetAllErrors(ValidationResult validationResult) =>
            validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

        public virtual Task<Response<O>> HandleAsync(I input)
        {
            throw new NotImplementedException();
        }

        public ValidationResult ValidateEntity<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : class => validation.Validate(entity);
    }
}
