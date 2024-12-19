using CatalogService.Application;

namespace CatalogService.API.Middlewares
{
    public static class ApplicationMiddlewares
    {
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 25;

        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplication();
            builder.AddDocumentationConfig();
            builder.AddJwtConfiguration();
        }
    }
}
