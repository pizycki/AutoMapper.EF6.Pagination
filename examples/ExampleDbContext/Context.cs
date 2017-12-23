using ExampleDbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleDbContext
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customers")
                .HasKey(x => x.Id);
        }
    }
}