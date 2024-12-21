using CatalogService.API.Configurations;
using CatalogService.Application;

namespace CatalogService.API.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplication();
            builder.AddDocumentationConfig();
            builder.AddJwtConfiguration();
        }
    }
}
