using CatalogService.Application.DTOs;
using CatalogService.Application.UseCases;
using CatalogService.Application.UseCases.Product.Create;
using CatalogService.Application.UseCases.Product.Delete;
using CatalogService.Application.UseCases.Product.GetAll;
using CatalogService.Application.UseCases.Product.GetById;
using CatalogService.Application.UseCases.Product.Update;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace CatalogService.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCase<ProductDTO, ProductDTO>, CreateUseCase>();
            services.AddTransient<IUseCase<ObjectId, ProductDTO>, DeleteUseCase>();
            services.AddTransient<IUseCase<ObjectId, GetProductDTO>, GetByIdUseCase>();
            services.AddTransient<IUseCase<UpdateRequest, ProductDTO>, UpdateUseCase>();
            services.AddTransient<IUseCase<GetAllRequest, List<GetProductDTO>>, GetAllUseCase>();
        }
    }
}
