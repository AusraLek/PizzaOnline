using Microsoft.AspNetCore.Mvc;
using PizzaOnline.Data;
using PizzaOnline.Data.Entities;
using PizzaOnline.Logic;
using PizzaOnline.Models;

namespace PizzaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaContext context;
        private readonly IPizzaPriceCalculator calculator;

        public PizzaController(PizzaContext context, IPizzaPriceCalculator calculator)
        {
            this.context = context;
            this.calculator = calculator;
        }

        [HttpGet("info")]
        public PizzaInfo Info()
        {
            var sizes = this.context.Sizes.Select(item => item.Size).ToArray();
            var toppings = this.context.Toppings.Select(item => item.Topping).ToArray();

            return new PizzaInfo
            {
                Sizes = sizes,
                Toppings = toppings,
            };
        }

        [HttpPost("order")]
        public void Order([FromBody] Order order)
        {
            var entity = new OrderEntity
            {
                Timestamp = DateTime.UtcNow,
                Size = order.SelectedSize,
                Toppings = string.Join(", ", order.SelectedToppings),
                Price = this.calculator.CalculateTotalPrice(order),
            };

            this.context.Orders.Add(entity);
            this.context.SaveChanges();
        }

        [HttpPost("price")]
        public double Price([FromBody] Order order)
        {
            return this.calculator.CalculateTotalPrice(order);
        }

        [HttpGet("orders")]
        public IEnumerable<OrderEntity> Orders()
        {
            return this.context.Orders.OrderByDescending(item => item.Timestamp).ToArray();
        }
    }
}
