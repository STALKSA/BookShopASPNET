using Microsoft.AspNetCore.Mvc;

namespace OnlineShop
{
    public class Catalog
    {
        private readonly List<Product> _products;
        private readonly Dictionary<int, Product> _productDictionary;

        public Catalog()
        {
            _products = GenerateProducts(10);
            _productDictionary = _products.ToDictionary(p => p.Id);
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public void AddProduct(Product product)
        {
            if (_productDictionary.ContainsKey(product.Id))
            {
                throw new ArgumentException($"Товар с ID {product.Id} уже существует.");
            }

            _products.Add(product);
            _productDictionary.Add(product.Id, product);
        }

        public Product GetProductById(int productId)
        {
            if (_productDictionary.TryGetValue(productId, out Product product))
            {
                return product;
            }

            return null;
        }

        public void RemoveProduct(Product product)
        {
            _products.Remove(product);
            _productDictionary.Remove(product.Id);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            if (!_productDictionary.ContainsKey(updatedProduct.Id))
            {
                throw new ArgumentException($"Продукт с ID {updatedProduct.Id} не существует");
            }
            
            var index = _products.FindIndex(p => p.Id == updatedProduct.Id);
            _products[index] = updatedProduct;
            _productDictionary[updatedProduct.Id] = updatedProduct;
        }

        public void UpdateProductById(int productId, Product updatedProduct)
        {
            if (!_productDictionary.ContainsKey(productId))
            {
                throw new ArgumentException($"Продукт с ID {productId} не существует");
            }

            updatedProduct.Id = productId;
            _products[_products.FindIndex(p => p.Id == productId)] = updatedProduct;
            _productDictionary[productId] = updatedProduct;
        }

        public void ClearCatalog()
        {
            _products.Clear();
            _productDictionary.Clear();
        }

        private static List<Product> GenerateProducts(int count)
        {
            var random = new Random();
            var products = new List<Product>();

            var productNames = new string[]
            {
            "Cracking the Coding Interview",
            "Code Complete",
            "Clean Code",
            "Refactoring",
            "Head First Design Patterns",
            "Patterns of Enterprise Application Architecture",
            "Working Effectively with Legacy Code",
            "The Clean Coder",
            "Introduction to Algorithms",
            "The Pragmatic Programmer"
            };

            for (int i = 0; i < count; i++)
            {
                var name = productNames[i];
                var price = random.Next(50, 500);
                var producedAt = DateTime.Now.AddDays(-random.Next(1, 30));
                var expiredAt = producedAt.AddDays(random.Next(1, 365));
                var stock = random.NextDouble() * 100;

                var product = new Product(i + 1, name, price)
                {
                    Description = "Описание " + name,
                    ProducedAt = producedAt,
                    ExpiredAt = expiredAt,
                    Stock = stock
                };

                products.Add(product);
            }

            return products.ToList();
        }

        ///<summary>
        /// REST запросы
        /// </summary>

        //// REST-эндпоинт для добавления нового товара в каталог
        //[HttpPost("/products")]
        //Product AddProduct([FromBody] Product product)
        //{
        //    catalog.AddProduct(product);
        //    return product;
        //}

        //// REST-эндпоинт для получения конкретного товара
        //[HttpGet("/products/{productId}")]
        //Product GetProduct(int productId)
        //{
        //    var products = catalog.GetProducts();
        //    if (productId < 0 || productId >= products.Count)
        //    {
        //        app.Response.StatusCode = 404; // Not Found
        //        return null;
        //    }

        //    return products[productId];
        //}

        //// REST-эндпоинт для удаления товара из каталога
        //[HttpDelete("/products/{productId}")]
        //void DeleteProduct(int productId)
        //{
        //    var products = catalog.GetProducts();
        //    if (productId < 0 || productId >= products.Count)
        //    {
        //        app.Response.StatusCode = 404; // Not Found
        //        return;
        //    }

        //    products.RemoveAt(productId);
        //}

        //// REST-эндпоинт для редактирования товара в каталоге
        //[HttpPut("/products/{productId}")]
        //void UpdateProduct(int productId, [FromBody] Product updatedProduct)
        //{
        //    var products = catalog.GetProducts();
        //    if (productId < 0 || productId >= products.Count)
        //    {
        //        app.Response.StatusCode = 404; // Not Found
        //        return;
        //    }

        //    products[productId] = updatedProduct;
        //}

        //// REST-эндпоинт для очистки каталога
        //[HttpDelete("/products")]
        //void ClearCatalog()
        //{
        //    catalog.ClearProducts();
        //}

    }

}
