using System;
using System.Data.SqlClient;
using System.Linq;
using PagiNET.IntegrationTests.SqlCommands;

namespace PagiNET.IntegrationTests.Setup
{
    public class CreateDatabase
    {
        private readonly DatabaseDirectoryManager _dbDirManager;
        private readonly DatabaseConfig _cfg;
        private readonly string[] _createTableScripts;

        public CreateDatabase(
            DatabaseConfig databaseConfig,
            DatabaseDirectoryManager dbDirManager,
            params string[] scripts)
        {
            _cfg = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
            _dbDirManager = dbDirManager ?? throw new ArgumentNullException(nameof(dbDirManager));
            _createTableScripts = scripts ?? Enumerable.Empty<string>().ToArray();
        }

        internal void Go()
        {
            if (_dbDirManager.DoesDatabaseDirectoryExists() == false)
                _dbDirManager.TryCreateDatabaseDirectory();

            TryCreateDatabase();
            RunTableCreateScripts();
        }

        private void RunTableCreateScripts() =>
            _createTableScripts.ToList().ForEach(RunScriptAgainstDatabase);

        private void RunScriptAgainstDatabase(string script) =>
            SqlCommandHelpers.ExecuteSqlCommand(_cfg.ExampleDatabaseConnString, script);

        private void TryCreateDatabase()
        {
            try
            {
                SqlCommandHelpers.ExecuteSqlCommand(_cfg.MasterConnString,
                    $@"CREATE DATABASE [{_cfg.DatabaseName}]
                       CONTAINMENT = NONE
                       ON  PRIMARY 
                       ( NAME = N'{_cfg.DatabaseName}', FILENAME = N'{_cfg.DatabasePath}' )");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("already exists"))
                    return;

                throw;
            }
        }
    }
}