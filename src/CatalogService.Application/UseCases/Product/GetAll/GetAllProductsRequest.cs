namespace CatalogService.Application.UseCases.Product.GetAll
{
    public class GetAllProductsRequest
    {
        public GetAllProductsRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
