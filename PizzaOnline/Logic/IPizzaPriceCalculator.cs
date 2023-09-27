using PizzaOnline.Models;

namespace PizzaOnline.Logic
{
    public interface IPizzaPriceCalculator
    {
        double CalculateTotalPrice(Order order);
    }
}