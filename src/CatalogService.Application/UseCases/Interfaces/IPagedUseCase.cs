using CatalogService.Application.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace CatalogService.Application.UseCases.Interfaces
{
    public interface IPagedUseCase<I, O>
    {
        Task<PagedResponse<O>> HandleAsync(I input);
    }
}
