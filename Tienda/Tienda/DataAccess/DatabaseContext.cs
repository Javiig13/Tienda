using Microsoft.EntityFrameworkCore;
using Tienda.Models;

namespace Tienda.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<Product> Products { get; set; }
    }
}
