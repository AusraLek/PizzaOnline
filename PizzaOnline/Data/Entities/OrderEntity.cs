namespace PizzaOnline.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Size { get; set; }

        public string Toppings { get; set; }

        public double Price { get; set; }
    }
}
