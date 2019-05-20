using Dlw.Techorama2019.Challenge.Model;
using Microsoft.EntityFrameworkCore;

namespace Dlw.Techorama2019.Challenge
{
    public class EFCoreContext : DbContext
    {
        private string ConnectionString { get; }

        public DbSet<Product> Products { get; set; }

        public EFCoreContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductProperty>()
                .HasKey(x => new { x.ProductId, x.Name });
        }
    }
}
