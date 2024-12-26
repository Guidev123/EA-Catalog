using CatalogService.Application.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Application.UseCases.Interfaces
{
    public interface IUseCase<I, O>
    {
        abstract Task<Response<O>> HandleAsync(I input);
    }
}
