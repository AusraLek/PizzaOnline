using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PizzaOnline.Data;
using PizzaOnline.Data.Entities;
using PizzaOnline.Logic;
using PizzaOnline.Models;

namespace PizzaOnline.Tests
{
    [TestClass]
    public class PizzaPriceCalculatorTests
    {
        private readonly Mock<PizzaContext> context;
        private readonly PizzaPriceCalculator calculator;

        public PizzaPriceCalculatorTests()
        {
            this.context = new Mock<PizzaContext>();
            this.calculator = new PizzaPriceCalculator(this.context.Object);
        }

        [TestMethod]
        [DataRow(20.2, 5, 3, 28.2)]
        [DataRow(1, 1, 1, 3)]
        [DataRow(0.2, 0.5, 0.7, 1.4)]
        public void CalculateTotalPrice(
            double sizePrice,
            double topping1Price,
            double topping2Price,
            double expectedResult)
        {
            // Arrange
            this.context
                .Setup<DbSet<PizzaSizeEntity>>(mock => mock.Sizes)
                .ReturnsDbSet(new[] { new PizzaSizeEntity { Size = "TestSize", Price = sizePrice } });

            this.context
                .Setup<DbSet<PizzaToppingsEntity>>(mock => mock.Toppings)
                .ReturnsDbSet(new[] {
                    new PizzaToppingsEntity { Topping = "Topping1", Price = topping1Price  },
                    new PizzaToppingsEntity { Topping = "Topping2", Price = topping2Price  },
                });

            var testOrder = new Order
            {
                SelectedSize = "TestSize",
                SelectedToppings = new[] { "Topping1", "Topping2" },
            };

            // Act
            var result = this.calculator.CalculateTotalPrice(testOrder);

            // Assert
            result
                .Should()
                .Be(expectedResult);
        }

        [TestMethod]
        public void ApplyDiscount()
        {
            // Arrange
            this.context
                .Setup<DbSet<PizzaSizeEntity>>(mock => mock.Sizes)
                .ReturnsDbSet(new[] { new PizzaSizeEntity { Size = "TestSize", Price = 8 } });

            this.context
                .Setup<DbSet<PizzaToppingsEntity>>(mock => mock.Toppings)
                .ReturnsDbSet(new[] {
                    new PizzaToppingsEntity { Topping = "Topping1", Price = 1  },
                    new PizzaToppingsEntity { Topping = "Topping2", Price = 1  },
                    new PizzaToppingsEntity { Topping = "Topping3", Price = 1  },
                    new PizzaToppingsEntity { Topping = "Topping4", Price = 1  },
                });

            var testOrder = new Order
            {
                SelectedSize = "TestSize",
                SelectedToppings = new[] { "Topping1", "Topping2", "Topping3", "Topping4" },
            };

            // Act
            var result = this.calculator.CalculateTotalPrice(testOrder);

            // Assert
            result
                .Should()
                .Be(10.8);
        }
    }
}
