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

app.MapGet("/get_catalog", GetCatalog);
app.MapPost("/add_product", AddProduct);
app.MapPost("/delete_product", RemoveProduct);
app.MapPost("/update_product", UpdateProduct);
app.MapPost("/clear_catalog", ClearCatalog;

async Task AddProduct(HttpContext context)
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

async Task<List<Product>> GetCatalog()
{
    return catalog.GetProductsAsync();
}

async Task RemoveProduct(Product product)
{
    return catalog.RemoveProductAsync(product);
}

async Task UpdateProduct(Product product)
{
    return catalog.UpdateProductAsync(product);
}

async Task ClearCatalog(HttpContext context)
{
    await catalog.ClearCatalogAsync();
    context.Response.StatusCode = StatusCodes.Status204NoContent;
}


app.Run();
