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
        private readonly string[] _createTableScripts;

        public CreateDatabase(DatabaseConfig databaseConfig, params string[] scripts)
        {
            _cfg = databaseConfig;
            _createTableScripts = scripts;
        }

        internal void Go()
        {
            TryCreateDatabase();
            RunTableCreateScripts();
        }

        private void RunTableCreateScripts()
        {
            foreach (var script in _createTableScripts)
                SqlCommandHelpers.ExecuteSqlCommand(_cfg.ExampleDatabaseConnectionString, script);
        }

        private void TryCreateDatabase()
        {
            try
            {
                SqlCommandHelpers.ExecuteSqlCommand(_cfg.MasterConnectionString,
                    $@"CREATE DATABASE [{_cfg.DatabaseName}]
                  CONTAINMENT = NONE
                  ON  PRIMARY 
                  ( NAME = N'{_cfg.DatabaseName}', FILENAME = N'{_cfg.DatabaseFileName}' )");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    return;
                }

                throw;
            }
        }
    }
}