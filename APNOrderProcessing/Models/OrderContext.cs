using Microsoft.EntityFrameworkCore;

namespace APNOrderProcessing.Models
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=orders.db");
        }
    }
}
