using Microsoft.EntityFrameworkCore;
using PizzaOnline.Data.Entities;

namespace PizzaOnline.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext()
        { 
        }

        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
        { 
        }

        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<PizzaSizeEntity> Sizes { get; set; }
        public virtual DbSet<PizzaToppingsEntity> Toppings { get; set; }
    }
}
