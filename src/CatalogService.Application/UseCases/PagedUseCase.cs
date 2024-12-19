using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;

namespace CatalogService.Application.UseCases
{
    public abstract class PagedUseCase<I, O> : BaseUseCase, IPagedUseCase<I, O>
    {
        public virtual Task<PagedResponse<O>> HandleAsync(I input) =>
            throw new NotImplementedException();
    }
}
