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
        public DbSet<Person> People { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("People")
                .HasKey(x => x.Id);
        }
    }
}