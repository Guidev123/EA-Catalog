using Azure.Storage.Blobs;
using CatalogService.Application.Storage;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Persistence.Repositories;
using CatalogService.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CatalogService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services, configuration);
            AddMongoMiddleware(services, configuration);
            AddRedisCache(services, configuration);
        }
        public static void AddMongoMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MongoDbService>();

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection");
                return new MongoClient(connectionString);
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("catalog-dev");
            });
        }

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton(_ => new BlobServiceClient(configuration.GetSection("BlobStorageConfig")["Connection"]));
        }

        public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(op =>
            {
                op.InstanceName = configuration["CacheDataSettings:InstanceName"];
                op.Configuration = configuration["CacheDataSettings:Configuration"];
            });
            services.AddTransient<ICacheService, CacheService>();
        }
    }
}
