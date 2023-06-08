using OnlineShop;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Catalog catalog = new Catalog();

app.MapGet("/get_catalog", GetCatalog);
app.MapPost("/add_product",AddProduct);

void AddProduct(Product product)
{
    catalog.AddProduct(product);
}

List<Product> GetCatalog()
{
    return catalog.GetProducts();
}

app.Run();
