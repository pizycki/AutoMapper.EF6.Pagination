using System.Data.SqlClient;
using ExampleDbContext;
using PagiNET.IntegrationTests.Config;

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
        private EnvConfig EnvConfig { get; }
        private DbContextProvider DbContextProvider { get; }
        private CreateDatabase CreateDatabase { get; }
        private DropDatabase DropDatabase { get; }
        private CreateTableScriptsProvider CreateTableScriptsProvider { get; }
        private DatabaseDirectoryManager DatabaseDirectoryManager { get; }

        public TestDatabaseManager()
        {
            EnvConfig = new EnvConfig();

            var dbCfg = new DatabaseConfig(EnvConfig);

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

    }

    public static class ConnectionStringHelper
    {
        public static SqlConnectionStringBuilder CreateMasterCatalogConnString(string dataSource) => CreateConnString(dataSource, "Master");

        public static SqlConnectionStringBuilder CreateConnString(string dataSource, string initCatalog) =>
            new SqlConnectionStringBuilder
            {
                DataSource = dataSource, // @"(LocalDB)\MSSQLLocalDB"
                InitialCatalog = initCatalog,
                IntegratedSecurity = true
            };

    }
}