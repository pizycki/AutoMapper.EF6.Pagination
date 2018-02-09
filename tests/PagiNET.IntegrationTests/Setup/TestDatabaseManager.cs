using System;
using System.Configuration;
using System.Data.SqlClient;
using ExampleDbContext;

namespace PagiNET.IntegrationTests.Setup
{
    public interface ITestDatabaseManager
    {
        void CreateDatabase();
        void DropDatabase();
        Context CreateDbContext();
    }

    public class TestDatabaseManager : ITestDatabaseManager
    {
        private DbContextProvider DbContextProvider { get; }
        private CreateDatabase CreateDatabase { get; }
        private DropDatabase DropDatabase { get; }
        private CreateTableScriptsProvider CreateTableScriptsProvider { get; }
        private DatabaseDirectoryManager DatabaseDirectoryManager { get; }

        public TestDatabaseManager()
        {
            var dbCfg = new DatabaseConfig(MasterConnString, ExampleDatabaseConnString, DatabaseName, DatabaseFileName);

            DbContextProvider = new DbContextProvider(dbCfg);
            CreateTableScriptsProvider = new CreateTableScriptsProvider();
            DatabaseDirectoryManager = new DatabaseDirectoryManager(dbCfg);
            CreateDatabase = new CreateDatabase(dbCfg, DatabaseDirectoryManager, CreateTableScriptsProvider.GetScripts());
            DropDatabase = new DropDatabase(dbCfg);
        }

        private void SeedCustomersTable()
        {
            using (var dbCtx = DbContextProvider.CreateDbContext())
                CustomersSeeder.Seed(dbCtx);
        }

        void ITestDatabaseManager.CreateDatabase()
        {
            DropDatabase.Go();
            CreateDatabase.Go();
            SeedCustomersTable();
        }

        void ITestDatabaseManager.DropDatabase() => DropDatabase.Go();

        Context ITestDatabaseManager.CreateDbContext() => DbContextProvider.CreateDbContext();

        private static string DatabaseName = "ExampleDatabase";
        private static string DatabaseFileName =>
            TryGetAppVeyorDatabasePath()
            ?? Environment.GetEnvironmentVariable("PAGINET_DATABASE_PATH")
            ?? ConfigurationManager.AppSettings["DatabasePath"]
            ?? Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\ExampleDatabase.mdf");

        private static string TryGetAppVeyorDatabasePath() =>
            Environment.GetEnvironmentVariable("APPVEYOR") == "True"
                ? Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER") + @"\ExampleDatabase.mdf"
                : null;

        private static string MasterConnString => Master.ToString();
        private static string ExampleDatabaseConnString => ExampleDatabase.ToString();

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "Master",
                IntegratedSecurity = true
            };

        private static SqlConnectionStringBuilder ExampleDatabase =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "ExampleDatabase",
                IntegratedSecurity = true
            };
    }
}
