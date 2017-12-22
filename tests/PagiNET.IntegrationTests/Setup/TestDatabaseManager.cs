﻿using System.Data.SqlClient;
using ExampleDbContext;

namespace PagiNET.IntegrationTests.Setup
{
    public class TestDatabaseManager : ITestDatabaseManager
    {
        private CreateDatabase CreateDatabase { get; }
        private DropDatabase DropDatabase { get; }
        private DbContextProvider DbContextProvider { get; }

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