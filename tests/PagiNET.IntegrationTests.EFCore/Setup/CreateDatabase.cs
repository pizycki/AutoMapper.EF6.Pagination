using ExampleDbContext;
using Microsoft.EntityFrameworkCore;
using PagiNET.IntegrationTests.EFCore.SqlCommands;

namespace PagiNET.IntegrationTests.EFCore.Setup
{
    public class CreateDatabase
    {
        private readonly DatabaseConfig _cfg;
        private readonly DbContextProvider _dbContextProvider;

        public CreateDatabase(DatabaseConfig databaseConfig, DbContextProvider dbContextProvider)
        {
            _cfg = databaseConfig;
            _dbContextProvider = dbContextProvider;
        }

        internal void Go(bool seed = false)
        {
            SqlCommandHelpers.ExecuteSqlCommand(_cfg.ConnectionString,
                $@"CREATE DATABASE [{_cfg.DatabaseName}]
                  CONTAINMENT = NONE
                  ON  PRIMARY 
                  ( NAME = N'{_cfg.DatabaseName}', FILENAME = N'{_cfg.DatabaseFileName}' )");

            var context = _dbContextProvider.CreateDbContext();
            context.Database.Migrate();
            if (seed)
                CustomersSeeder.Seed(context);

        }
    }
}