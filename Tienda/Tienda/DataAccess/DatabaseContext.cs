using Microsoft.EntityFrameworkCore;
using System.Net;
using Tienda.Models;

namespace Tienda.DataAccess
{
    public class DatabaseContext : DbContext
    {
        private readonly WebClient webClient;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            webClient = new WebClient();
        }

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

            // Seed Database
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Mouse Gaming Razer", Price = 65, Stock = 10000, Image = webClient.DownloadData("https://www.f21.uy/imgs/productos/productos31_52431.jpg") },
                new Product() { Id = 2, Name = "Monitor HPw20", Price = 45, Stock = 800, Image = webClient.DownloadData("https://cdn.pcpartpicker.com/static/forever/images/product/68f756c36cc6c9055f329a1ca93885fb.256p.jpg") },
                new Product() { Id = 3, Name = "Intel Core i7-9700K", Price = 287, Stock = 1500, Image = webClient.DownloadData("https://www.infinit.com.uy/imgs/representaciones/foto31_20.jpg") },
                new Product() { Id = 4, Name = "AMD Ryzen 2700", Price = 150, Stock = 20000, Image = webClient.DownloadData("https://img.web4pro.es/articulos/256/256/fixed/art_amd-ryzen%20yd2600bbafbox_1.jpg") },
                new Product() { Id = 5, Name = "GeForce GTX 1050 Ti", Price = 198, Stock = 14000, Image = webClient.DownloadData("https://cdn.pcpartpicker.com/static/forever/images/product/d66664842af45d6fdb44cec72571512c.256p.jpg") },
                new Product() { Id = 6, Name = "Keyboard Corsair K68", Price = 168, Stock = 7500, Image = webClient.DownloadData("https://media-esp-buyviu-com.s3.amazonaws.com/products/198b63897c3c0c357c901bf3467cf169_image_1_thumb.png") },
                new Product() { Id = 7, Name = "PS4 500GB", Price = 220, Stock = 320, Image = webClient.DownloadData("https://hitechevolution.files.wordpress.com/2018/04/ps4-pro-listing-thumb-01-ps4-eu-06sep16.png?w=256&h=256&crop=1") },
                new Product() { Id = 8, Name = "Smartwatch Apple", Price = 450, Stock = 255, Image = webClient.DownloadData("https://img.web4pro.es/articulos/256/256/fixed/art_apl-watch%20s4%20mtx52tybardera_1.jpg") },
                new Product() { Id = 9, Name = "Xiaomi Mi8 128GB", Price = 365, Stock = 48, Image = webClient.DownloadData("https://img.web4pro.es/articulos/256/256/fixed/art_xia-sp%20mi%208%20lite%20ds%206%20128%20nm_1.jpg") },
                new Product() { Id = 10, Name = "10Kg Potatoes", Price = 6, Stock = 20, Image = webClient.DownloadData("https://pbs.twimg.com/profile_images/595595491246747648/A2_2wMqw_400x400.jpg") });

            modelBuilder.Entity<Customer>().HasData(
                new Customer() { Id = 1, Username = "Javi", Password = "upm.net", Phone = "915642321", UserRole = UserRole.Administrator },
                new Customer() { Id = 2, Username = "Luis", Password = "upm.net", Phone = "915642322", UserRole = UserRole.Client });
        }
    }
}
