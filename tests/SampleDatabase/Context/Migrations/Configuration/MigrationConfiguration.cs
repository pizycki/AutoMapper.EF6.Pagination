using System.Data.Entity.Migrations;

namespace SampleDatabase.Context.Migrations.Configuration
{
    /**
     * Use this class to manage migrations
     * Paste following commands into Package Manager Console
     * (Make sure "Default project" is set to this project)
     * 
     * Add new migration:
     * Add-Migration -Name 1.0.0
     * 
     * Update database to the latest version:
     * Update-Database
     */
     
    public class MigrationConfiguration : DbMigrationsConfiguration<CompanyDbContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = false;
            ContextKey = HardCodedContextKey;
            MigrationsNamespace = "SampleDatabase.Context.Migrations";
            MigrationsDirectory = @"Context\Migrations";
        }

        protected override void Seed(CompanyDbContext context)
        {
        }

        private const string HardCodedContextKey = "C714FCC1-270B-4BAA-8A67-B30A4D22F1AC";
    }
}
