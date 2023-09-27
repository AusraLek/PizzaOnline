using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PizzaOnline.Controllers;
using PizzaOnline.Data;
using PizzaOnline.Data.Entities;
using PizzaOnline.Logic;
using PizzaOnline.Models;
using System.Reflection.Metadata;

namespace PizzaOnline.Tests
{
    [TestClass]
    public class PizzaControllerTests
    {
        private readonly Mock<PizzaContext> context;
        private readonly Mock<IPizzaPriceCalculator> calculator;
        private readonly PizzaController controller;

        public PizzaControllerTests()
        {
            this.context = new Mock<PizzaContext>();
            this.calculator = new Mock<IPizzaPriceCalculator>();
            this.controller = new PizzaController(this.context.Object, this.calculator.Object);
        }

        [TestMethod]
        public void PizzaInfoFromDatabase()
        {
            //Arrange
            this.context.Setup<DbSet<PizzaSizeEntity>>(mock => mock.Sizes)
                .ReturnsDbSet(new[] {
                    new PizzaSizeEntity { Size = "TestSize", Price = 8 },
                    new PizzaSizeEntity { Size = "SizeAbc", Price = 7 },
                });

            this.context.Setup<DbSet<PizzaToppingsEntity>>(mock => mock.Toppings)
                .ReturnsDbSet(new[] {
                    new PizzaToppingsEntity { Topping = "Topping1", Price = 1  },
                    new PizzaToppingsEntity { Topping = "Topping2", Price = 1  },
                    new PizzaToppingsEntity { Topping = "Topping3", Price = 1  },
                    new PizzaToppingsEntity { Topping = "Topping4", Price = 1  },
                });

            //Act
            var result = this.controller.Info();

            //Assert
            result
                .Should()
                .BeEquivalentTo(new PizzaInfo
                {
                    Sizes = new[] { "TestSize", "SizeAbc" },
                    Toppings = new[] { "Topping1", "Topping2", "Topping3", "Topping4" },
                });
        }

        [TestMethod]
        public void PlaceOrder()
        {
            //Arrange
            var mockSet = new Mock<DbSet<OrderEntity>>();
            this.context.Setup(mock => mock.Orders).Returns(mockSet.Object);

            var testOrder = new Order 
            {
                SelectedSize = "test",
                SelectedToppings = new[] { "test" },
            };

            //Act
            this.controller.Order(testOrder);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<OrderEntity>()), Times.Once());
            this.context.Verify(m => m.SaveChanges(), Times.Once());
            this.calculator.Verify(m => m.CalculateTotalPrice(It.IsAny<Order>()), Times.Once());
        }

        [TestMethod]
        public void CalculatePrice()
        {
            //Arrange
            this.calculator.Setup(mock => mock.CalculateTotalPrice(It.IsAny<Order>()))
                .Returns(12345.6789);

            var testOrder = new Order
            {
                SelectedSize = "test",
                SelectedToppings = new[] { "test" },
            };

            //Act
            var result = this.controller.Price(testOrder);

            //Assert
            result
                .Should()
                .Be(12345.6789);

            this.calculator.Verify(m => m.CalculateTotalPrice(It.IsAny<Order>()), Times.Once());
        }

        [TestMethod]
        public void ReturnOrders()
        {
            //Arrange
            var testOrders = new[]
            {
                new OrderEntity(),
                new OrderEntity(),
                new OrderEntity(),
            };
            this.context.Setup<DbSet<OrderEntity>>(mock => mock.Orders)
                .ReturnsDbSet(testOrders);

            //Act
            var result = this.controller.Orders();

            //Assert
            result
                .Should()
                .BeEquivalentTo(testOrders);
        }
    }
}
