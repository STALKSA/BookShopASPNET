namespace OnlineShop
{
    public class Product
    {
        private static Dictionary<int, Product> productDictionary = new Dictionary<int, Product>();

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (productDictionary.ContainsKey(value))
                {
                    throw new ArgumentException($"Товар с ID {value} уже существует.");
                }
                else
                {
                    if (id != value)
                    {
                        productDictionary.Remove(id);
                        productDictionary.Add(value, this);
                        id = value;
                    }
                }
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ProducedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public double Stock { get; set; }

        public Product(int id, string name, decimal price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' не может быть пустым");
            }
            if (price <= 0)
            {
                throw new ArgumentException($"'{nameof(price)}' не может быть меньше или равно 0");
            }

            Id = id;
            Name = name;
            Price = price;
            productDictionary.Add(id, this);
        }
    }

}
