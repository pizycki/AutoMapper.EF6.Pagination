using System.Data.Entity;
using System.Reflection;
using SampleDatabase.Context.Entities;

namespace SampleDatabase.Context
{
    public sealed class CompanyDbContext : DbContext
    {
        #region ////// DbSets //////

        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> People { get; set; }

        #endregion

        public CompanyDbContext() : base("SampleDatabase") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // No need to call from base, carry on

            modelBuilder.Configurations.AddFromAssembly(SampleDbContexAssembly);
        }

        private static Assembly SampleDbContexAssembly => typeof(CompanyDbContext).Assembly;

        public static CompanyDbContext Create() => new CompanyDbContext();
    }
}