using Microsoft.EntityFrameworkCore;

namespace AngularExample.EF
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) 
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customers")
                .HasKey(x => x.Id);
        }
    }
}