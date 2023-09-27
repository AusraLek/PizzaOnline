namespace PizzaOnline.Models
{
    public class Order
    {
        public string SelectedSize { get; set; }

        public IEnumerable<string> SelectedToppings { get; set; }
    }
}
