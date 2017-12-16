using System.Data.SqlClient;
using ExampleDbContext;
using PagiNET.IntegrationTests.EFCore.Tests;

namespace PagiNET.IntegrationTests.EFCore.Setup
{
    public class TestDatabaseManager : ITestDatabaseManager
    {
        public static TestDatabaseManager Create() => new TestDatabaseManager();

        private CreateDatabase CreateDatabase { get; }
        private DropDatabase DropDatabase { get; }
        private DbContextProvider DbContextProvider { get; }

        private TestDatabaseManager()
        {
            var dbCfg = new DatabaseConfig(MasterConnString, DatabaseName, DatabaseFileName);

            CreateDatabase = new CreateDatabase(dbCfg);
            DropDatabase = new DropDatabase(dbCfg);
            DbContextProvider = new DbContextProvider(dbCfg);
        }

        void ITestDatabaseManager.CreateDatabase()
        {
            DropDatabase.Go();
            CreateDatabase.Go(seed: true);
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
        private static string DatabaseFileName => @"C:\Users\pizycki\SampleDatabase.mdf"; // TODO Change with configuration
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
