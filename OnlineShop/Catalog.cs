namespace OnlineShop
{
    public class Catalog
    {
        private readonly List<Product> _products = GenerateProducts(10);

        public List<Product> GetProducts() 
        {
            return _products;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }


        private static List<Product> GenerateProducts(int count)
        {
            var random = new Random();
            var products = new Product[count];

            // Массив реальных названий товаров
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
            "The Pragmatic Programmer "
            };

            for (int i = 0; i < count; i++)
            {
                var name = productNames[i];
                var price = random.Next(50, 500);
                var producedAt = DateTime.Now.AddDays(-random.Next(1, 30));
                var expiredAt = producedAt.AddDays(random.Next(1, 365));
                var stock = random.NextDouble() * 100;

                products[i] = new Product(name, price)
                {
                    Description = "Описание " + name,
                    ProducedAt = producedAt,
                    ExpiredAt = expiredAt,
                    Stock = stock
                };
            }

            return products.ToList();
        }
    }
           
}
