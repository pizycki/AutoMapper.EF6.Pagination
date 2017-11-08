using System.Data.Entity;
using System.Reflection;
using SampleDatabase.SampleDbContext.Entities;

namespace SampleDatabase.SampleDbContext
{
    public sealed class SampleDbContext : DbContext
    {
        #region ////// DbSets //////

        public DbSet<Foo> Foos { get; set; }
        public DbSet<Bar> Bars { get; set; }

        #endregion

        public SampleDbContext() : base("EdmxConverter_SampleDbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // No need to call from base, carry on

            modelBuilder.Configurations.AddFromAssembly(SampleDbContexAssembly);
        }

        private static Assembly SampleDbContexAssembly => typeof(SampleDbContext).Assembly;
    }
}