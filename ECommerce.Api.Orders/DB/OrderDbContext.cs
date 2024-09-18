using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.DB
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
