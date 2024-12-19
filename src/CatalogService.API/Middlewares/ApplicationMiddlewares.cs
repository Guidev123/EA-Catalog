using CatalogService.Application;

namespace CatalogService.API.Middlewares
{
    public static class ApplicationMiddlewares
    {
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplication();
            builder.AddDocumentationConfig();
            builder.AddJwtConfiguration();
        }
    }
}
