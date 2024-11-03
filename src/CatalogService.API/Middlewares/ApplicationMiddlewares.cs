using CatalogService.API.Services;
using CatalogService.Domain.Services;

namespace CatalogService.API.Middlewares
{
    public static class ApplicationMiddlewares
    {
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 25;

        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.RegisterServices();
        }

        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IProductService, ProductService>();
        }
    }
}
