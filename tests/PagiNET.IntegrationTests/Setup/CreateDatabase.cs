using System;
using System.Data.SqlClient;
using ExampleDbContext;
using Microsoft.EntityFrameworkCore;
using PagiNET.IntegrationTests.SqlCommands;

namespace PagiNET.IntegrationTests.Setup
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

        internal void Go()
        {
            //try
            //{
            //    SqlCommandHelpers.ExecuteSqlCommand(_cfg.ConnectionString,
            //    $@"CREATE DATABASE [{_cfg.DatabaseName}]
            //      CONTAINMENT = NONE
            //      ON  PRIMARY 
            //      ( NAME = N'{_cfg.DatabaseName}', FILENAME = N'{_cfg.DatabaseFileName}' )");

            //}
            //catch (SqlException ex)
            //{
            //    if (ex.Message.Contains("already exists"))
            //    {
            //        return;
            //    }

            //    throw;
            //}

            var context = _dbContextProvider.CreateDbContext();
            context.Database.EnsureCreated();
        }
    }
}