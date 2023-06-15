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
app.MapPost("/clear_catalog", ClearCatalog);

async void AddProduct(HttpContext context)
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

List<Product> GetCatalog()
{
    return catalog.GetProducts();
}

void RemoveProduct(Product product)
{
    catalog.RemoveProduct(product);
}

void UpdateProduct(Product product)
{
    catalog.UpdateProduct(product);
}

void ClearCatalog(HttpContext context)
{
    catalog.ClearCatalog();
    context.Response.StatusCode = StatusCodes.Status204NoContent;
}


app.Run();
