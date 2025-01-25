using System.Text.Json.Serialization;

namespace CatalogService.Application.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(
            int totalCount,
            bool isSuccess,
            TData? data = default,
            int currentPage = 1,
            int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE,
            int code = 200,
            string? message = null,
            string[]? errors = null)
            : base(isSuccess, code, data, message, errors)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(
            bool isSuccess,
            TData? data,
            int code = 200,
            string? message = null,
            string[]? errors = null)
            : base(isSuccess, code, data, message, errors)
        {
        }

        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = ApplicationModule.DEFAULT_PAGE_SIZE;
        public int TotalCount { get; set; }
    }
}
