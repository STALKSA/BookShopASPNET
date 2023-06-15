using OnlineShop;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(
   options =>
   {
       options.SerializerOptions.WriteIndented = true;
   }
);
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

Catalog catalog = new Catalog();

app.MapGet("/get_catalog", GetProductsAsync);
app.MapPost("/add_product", AddProductAsync);
app.MapPost("/delete_product", RemoveProductAsync);
app.MapPost("/update_product", UpdateProductAsync);
app.MapPost("/clear_catalog", ClearCatalogAsync);

async Task AddProductAsync(HttpContext context)
{
    try
    {
        var product = await context.Request.ReadFromJsonAsync<Product>();

        catalog.AddProduct(product);
        var productId = product.Id;

        var uri = $"/get_product?productId={productId}";

        // код ответа 201 Created
        context.Response.StatusCode = StatusCodes.Status201Created;
        context.Response.Headers.Add("Location", uri);
    }
    catch (ArgumentException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync(ex.Message);
    }
}

async Task<List<Product>> GetProductsAsync()
{
    return catalog.GetProductsAsync();
}

async Task RemoveProductAsync(Product product)
{
    return catalog.RemoveProductAsync(product);
}

async Task UpdateProductAsync(Product product)
{
    return catalog.UpdateProductAsync(product);
}

async Task ClearCatalogAsync(HttpContext context)
{
    await catalog.ClearCatalogAsync();
    context.Response.StatusCode = StatusCodes.Status204NoContent;
}


app.Run();
