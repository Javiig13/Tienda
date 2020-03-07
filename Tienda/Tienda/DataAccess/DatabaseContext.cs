using Microsoft.EntityFrameworkCore;
using Tienda.Models;

namespace Tienda.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.OrderId, po.ProductId });
            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.OrderId);
            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Product)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Username).IsUnique();
        }
    }
}
