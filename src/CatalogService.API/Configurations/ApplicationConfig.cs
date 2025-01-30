using CatalogService.API.Configurations;
using CatalogService.Application;
using SharedLib.Tokens.Configuration;

namespace CatalogService.API.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplication();
            builder.AddDocumentationConfig();
            builder.Services.AddJwtConfiguration(builder.Configuration);
            builder.Services.AddAuthorization();
        }
    }
}
