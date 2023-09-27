using PizzaOnline.Data.Entities;

namespace PizzaOnline.Data
{
    public class DbInitializer
    {
        public static void Initialize(PizzaContext context)
        {
            context.Sizes.AddRange(new[]
            {
                new PizzaSizeEntity{ Size = "S", Price = 8 },
                new PizzaSizeEntity{ Size = "M", Price = 10 },
                new PizzaSizeEntity{ Size = "L", Price = 12 },
            });

            context.Toppings.AddRange(new[]
            {
                new PizzaToppingsEntity{ Topping = "Beef", Price = 1 },
                new PizzaToppingsEntity{ Topping = "Chicken", Price = 1 },
                new PizzaToppingsEntity{ Topping = "Salami", Price = 1 },
                new PizzaToppingsEntity{ Topping = "Tomato", Price = 1 },
                new PizzaToppingsEntity{ Topping = "Cheese", Price = 1 },
            });

            context.SaveChanges();
        }
    }
}
