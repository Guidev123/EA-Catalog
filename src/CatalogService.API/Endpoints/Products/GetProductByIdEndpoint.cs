﻿using CatalogService.Application.DTOs;
using CatalogService.Application.Responses;
using CatalogService.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CatalogService.API.Endpoints.Products
{
    public class GetProductByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/{id}", HandleAsync).Produces<Response<GetProductDTO>>();
        public static async Task<IResult> HandleAsync([FromServices] IUseCase<Guid, GetProductDTO> productUseCase,
                                                      Guid id)
        {
            var result = await productUseCase.HandleAsync(id);

            if (result.IsSuccess && result.Data is not null)
                return TypedResults.Ok(new Response<GetProductDTO>(result.Data, 200));

            return TypedResults.BadRequest(result);
        }
    }
}
