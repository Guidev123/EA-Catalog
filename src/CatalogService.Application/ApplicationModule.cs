using CatalogService.Application.DTOs;
using CatalogService.Application.UseCases.Interfaces;
using CatalogService.Application.UseCases.Product.Create;
using CatalogService.Application.UseCases.Product.Delete;
using CatalogService.Application.UseCases.Product.GetAll;
using CatalogService.Application.UseCases.Product.GetById;
using CatalogService.Application.UseCases.Product.GetByIds;
using CatalogService.Application.UseCases.Product.Update;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace CatalogService.Application
{
    public static class ApplicationModule
    {
        public const int DEFAULT_PAGE_NUMBER = 1;
        public const int DEFAULT_PAGE_SIZE = 25;
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCase<ProductDTO, string?>, CreateProductHandler>();
            services.AddTransient<IUseCase<Guid, ProductDTO>, DeleteProductHandler>();
            services.AddTransient<IUseCase<Guid, GetProductDTO>, GetProductByIdHandler>();
            services.AddTransient<IUseCase<UpdateProductRequest, ProductDTO>, UpdateProductHandler>();
            services.AddTransient<IUseCase<GetByIdsRequest, List<GetProductDTO>>, GetByIdsHandler>();
            services.AddTransient<IPagedUseCase<GetAllProductsRequest, List<GetProductDTO>>, GetAllProductsHandler>();
        }
    }
}
