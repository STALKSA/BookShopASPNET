namespace OnlineShop
{
    public class Product
    {

        public Product(string name, decimal price) 
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be empty");
            }
            if(price < 0)
            {
                throw new ArgumentException($"'{nameof(price)}' cannot be under 0");
            }

            Name = name;
            Price = price;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ProducedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public double Stock { get; set; }
         
    }
}
