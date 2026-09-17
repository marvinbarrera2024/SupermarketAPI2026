using Microsoft.OpenApi.Models;
using SupermarketAPI.DTOs;
using SupermarketAPI.Services.Brands;

namespace SupermarketAPI.Endpoints
{
    public static class BrandEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/brands").WithTags("Brands");

            group.MapGet("/", async (IBrandServices brandServices) =>
            {
                var brands = await brandServices.GetBrands();
                return Results.Ok(brands);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Marcas",
                Description = "Muestra una lista de todas las marcas."
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, IBrandServices brandServices) =>
            {
                var brand = await brandServices.GetBrand(id);
                return brand == null ? Results.NotFound() : Results.Ok(brand);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Marca",
                Description = "Busca una marca por id."
            }).RequireAuthorization();

            group.MapPost("/", async (BrandRequest brand, IBrandServices brandServices) =>
            {
                if (brand == null)
                    return Results.BadRequest();

                var id = await brandServices.PostBrand(brand);
                return Results.Created($"api/brands/{id}", brand);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Marca",
                Description = "Crea una nueva marca."
            }).RequireAuthorization();

            group.MapPut("/{id}", async (int id, BrandRequest brand, IBrandServices brandServices) =>
            {
                var result = await brandServices.PutBrand(id, brand);
                return result == -1 ? Results.NotFound() : Results.Ok(result);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modificar Marca",
                Description = "Actualiza una marca existente."
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IBrandServices brandServices) =>
            {
                var result = await brandServices.DeleteBrand(id);
                return result == -1 ? Results.NotFound() : Results.NoContent();
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar Marca",
                Description = "Elimina una marca existente."
            }).RequireAuthorization();
        }
    }
}