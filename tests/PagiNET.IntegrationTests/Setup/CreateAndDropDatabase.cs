using System.Data.Entity;
using System.IO;
using System.Linq;
using SampleDatabase.Context;
using SampleDatabase.Context.Migrations.Configuration;
using static RealDatabaseTests.SqlCommands.SqlCommandHelpers;

namespace RealDatabaseTests.Setup
{
    public class CreateAndDropDatabase
    {
        private readonly Config _cfg;

        public CreateAndDropDatabase(Config config) => _cfg = config;

        internal void CreateDatabase()
        {
            ExecuteSqlCommand(_cfg.ConnectionString,
                $@"CREATE DATABASE [{_cfg.DatabaseName}]
                  CONTAINMENT = NONE
                  ON  PRIMARY 
                  ( NAME = N'{_cfg.DatabaseName}', FILENAME = N'{_cfg.DatabaseFileName}' )");

            var migration = new MigrateDatabaseToLatestVersion<CompanyDbContext, MigrationConfiguration>();
            migration.InitializeDatabase(CompanyDbContext.Create());
        }

        public void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(_cfg.ConnectionString, $@"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('{_cfg.DatabaseName}')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(_cfg.ConnectionString, $@"
                    ALTER DATABASE [{_cfg.DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db '{_cfg.DatabaseName}'");

                fileNames.ForEach(File.Delete);
            }
        }

        public class Config
        {
            public Config(string connectionString, string databaseName, string databaseFileName)
            {
                ConnectionString = connectionString;
                DatabaseName = databaseName;
                DatabaseFileName = databaseFileName;
            }

            public string ConnectionString { get; }
            public string DatabaseName { get; }
            public string DatabaseFileName { get; }
        }
    }
}