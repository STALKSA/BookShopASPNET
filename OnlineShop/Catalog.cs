using Microsoft.AspNetCore.Mvc;

namespace OnlineShop
{
    public class Catalog
    {
        private ConcurrentDictionary<int, Product> _productDictionary;

        public Catalog()
        {
           _productDictionary = new ConcurrentDictionary<int, Product>(GenerateProducts(10).ToDictionary(p => p.Id));
        }

        public async Task<ConcurrentDictionary<int, Product>> GetProductsAsync()
        {
            return _productDictionary;
        }

        public async Task AddProductAsync(Product product)
        {
            if (!_productDictionary.TryAdd(product.Id, product))
        {
            throw new ArgumentException($"Товар с ID {product.Id} уже существует.");
        }
            
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            if (!_products.TryGetValue(productId, out var product))
            {
                throw new KeyNotFoundException($"Товар с ID '{productId}' не найден или не существует.");
            }
            else
            {
                return product;
            }
        }

        public async Task RemoveProduct(Product product)
        {
           _productDictionary.TryRemove(product.Id, out _);
        }

        public async Task UpdateProduct(Product updatedProduct)
        {
            if (!_productDictionary.ContainsKey(updatedProduct.Id))
        {
            throw new ArgumentException($"Продукт с ID {updatedProduct.Id} не существует");
        }

             _productDictionary[updatedProduct.Id] = updatedProduct;
        }

        public async Task UpdateProductById(int productId, Product updatedProduct)
        {
            if (!_productDictionary.ContainsKey(productId))
        {
            throw new ArgumentException($"Продукт с ID {productId} не существует");
        }

            updatedProduct.Id = productId;
            _productDictionary[productId] = updatedProduct;
        }

        public async Task ClearCatalog()
        {
            _productDictionary.Clear();
        }

        private static ConcurrentDictionary<int, Product> GenerateProducts(int count)
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

    }

}
