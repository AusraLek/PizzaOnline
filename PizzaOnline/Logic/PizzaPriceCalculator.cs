using PizzaOnline.Data;
using PizzaOnline.Models;

namespace PizzaOnline.Logic
{
    public class PizzaPriceCalculator : IPizzaPriceCalculator
    {
        private readonly PizzaContext context;

        public PizzaPriceCalculator(PizzaContext context)
        {
            this.context = context;
        }

        public double CalculateTotalPrice(Order order)
        {
            var sizePrice = this.context.Sizes
                .Where(item => item.Size == order.SelectedSize)
                .FirstOrDefault();

            var toppingsPrice = this.context.Toppings
                .Where(item => order.SelectedToppings.Contains(item.Topping))
                .ToArray();

            var totalPrice = sizePrice.Price + toppingsPrice.Select(item => item.Price).Sum();

            if (toppingsPrice.Length > 3)
            {
                return totalPrice * 0.9;
            }

            return totalPrice;
        }
    }
}
