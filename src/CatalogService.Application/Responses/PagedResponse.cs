using System.Text.Json.Serialization;

namespace CatalogService.Application.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        public PagedResponse() { }
        public PagedResponse(
            int totalCount,
            TData? data = default,
            int currentPage = 1,
            int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE,
            int code = DEFAULT_STATUS_CODE,
            string? message = null,
            string[]? errors = null)
            : base(data, code, message, errors)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(
            TData? data,
            int code = DEFAULT_STATUS_CODE,
            string? message = null,
            string[]? errors = null)
            : base(data, code, message, errors)
        {

        }

        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = ApplicationModule.DEFAULT_PAGE_SIZE;
        public int TotalCount { get; set; }
    }
}
