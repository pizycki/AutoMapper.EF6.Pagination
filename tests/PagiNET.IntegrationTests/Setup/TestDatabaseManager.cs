using System;
using System.Configuration;
using System.Data.SqlClient;
using ExampleDbContext;

namespace PagiNET.IntegrationTests.Setup
{
    public class TestDatabaseManager : ITestDatabaseManager
    {
        private DbContextProvider DbContextProvider { get; }
        private CreateDatabase CreateDatabase { get; }
        private DropDatabase DropDatabase { get; }

        public TestDatabaseManager()
        {
            var dbCfg = new DatabaseConfig(MasterConnString, DatabaseName, DatabaseFileName);

            DbContextProvider = new DbContextProvider(dbCfg);
            CreateDatabase = new CreateDatabase(dbCfg, DbContextProvider);
            DropDatabase = new DropDatabase(dbCfg);
        }

        void ITestDatabaseManager.CreateDatabase()
        {
            DropDatabase.Go();
            CreateDatabase.Go();
        }

        void ITestDatabaseManager.DropDatabase()
        {
            DropDatabase.Go();
        }

        Context ITestDatabaseManager.CreateDbContext()
        {
            return DbContextProvider.CreateDbContext();
        }

        private static string DatabaseName = "SampleDatabase";
        private static string DatabaseFileName =>
            Environment.GetEnvironmentVariable("PAGINET_DATABASE_PATH")
            ?? ConfigurationManager.AppSettings["DatabasePath"]
            ?? @"C:\Users\pawelizycki\SampleDatabase.mdf";
        private static string MasterConnString => Master.ToString();

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "Master",
                IntegratedSecurity = true
            };
    }
}
