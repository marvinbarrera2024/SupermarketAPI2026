using Microsoft.OpenApi.Models;
using SupermarketAPI.DTOs;
using SupermarketAPI.Services.Products;

namespace SupermarketAPI.Endpoints
{
    public static class ProductEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes) {
            var group = routes.MapGroup("/api/products").WithTags("Products");

            group.MapGet("/", async (IProductServices productServices) =>
            {
                var products = await productServices.GetProducts();
                //200 OK: La solicitud se realizó correctamente
                //y devuelve la lista de productos
                return Results.Ok(products);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Productos",
                Description = "Muestra una lista de todos los productos."
            }).RequireAuthorization();

            group.MapGet("/{id}", async (int id, IProductServices productServices) => {
                var product = await productServices.GetProduct(id);
                if (product == null) 
                    return Results.NotFound(); // 404 Not Found: El recurso solicitado no existe
                else
                    return Results.Ok(product); //200 OK: La solicitud se realizó correctamente y devuelve el producto
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Obtener Producto",
                Description = "Busca un producto por id."
            }).RequireAuthorization();

            group.MapPost("/", async (ProductRequest product, IProductServices productServices) => { 
                if(product == null)
                    return Results.BadRequest(); // 400 Bad Request: La solicitud no se pudo procesar, error de formato 

                var id = await productServices.PostProduct(product);
                //201 Created: El recurso se creó con éxito, se devuelve la úbicación del recurso creado
                return Results.Created($"api/products/{id}", product);
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Crear Producto",
                Description = "Crear un nuevo producto."
            }).RequireAuthorization();

            group.MapPut("/{id}", async (int id, ProductRequest product, IProductServices productServices) => {                
                var result = await productServices.PutProduct(id, product);
                if(result == -1)
                    return Results.NotFound(); // 404 Not Found: El recurso solicitado no existe
                else
                    return Results.Ok(result); //200 OK: La solicitud se realizó correctamente 
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Modifciar Producto",
                Description = "Actualiza un producto existente."
            }).RequireAuthorization();

            group.MapDelete("/{id}", async (int id, IProductServices productServices) => {
                var result = await productServices.DeleteProduct(id);
                if (result == -1)
                    return Results.NotFound(); // 404 Not Found: El recurso solicitado no existe
                else
                    return Results.NoContent(); //204 No Content: Recurso eliminado.
            }).WithOpenApi(o => new OpenApiOperation(o)
            {
                Summary = "Eliminar Producto",
                Description = "Eliminar un producto existente."
            }).RequireAuthorization();
        }
    }
}
