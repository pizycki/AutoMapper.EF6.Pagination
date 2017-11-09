using System.Data.Entity.Migrations;

namespace SampleDatabase.DbContext.Migrations.Configuration
{
    /**
     * Use this class to manage migrations
     * Paste following commands into Package Manager Console
     * (Make sure "Default project" is set to this project)
     * 
     * Add new migration:
     * Add-Migration -Name 1.0.0 -ConfigurationTypeName SampleDatabase.SampleDbContext.Migrations.Configuration.MigrationConfiguration
     * 
     * Update database to the latest version:
     * Update-Database -ConfigurationTypeName SampleDatabase.SampleDbContext.Migrations.Configuration.MigrationConfiguration
     */

    // ReSharper disable once UnusedMember.Global
    public class MigrationConfiguration : DbMigrationsConfiguration<SampleDbContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = false;
            ContextKey = SampleDbContext_ContextKey;
            MigrationsNamespace = "SampleDatabase.DbContext.Migrations";
            MigrationsDirectory = @"DbContext\Migrations";
        }

        protected override void Seed(SampleDbContext context)
        {
            new SampleDbContextSeeder().Seed(context);
        }

        private const string SampleDbContext_ContextKey = "C714FCC1-270B-4BAA-8A67-B30A4D22F1AC";
    }
}
