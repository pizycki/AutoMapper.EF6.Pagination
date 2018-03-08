using ExampleDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExampleDbContext
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExampleDatabase;Trusted_Connection=True;");

            return new Context(optionsBuilder.Options);
        }
    }

    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Director> Directors { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("People")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Customer>().HasBaseType<Person>();
            modelBuilder.Entity<Employer>().HasBaseType<Person>();
            modelBuilder.Entity<Director>().HasBaseType<Person>();
            modelBuilder.Entity<Manager>().HasBaseType<Person>();
        }
    }
}