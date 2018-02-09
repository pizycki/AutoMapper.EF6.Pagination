using System;
using System.Data.SqlClient;
using System.IO;
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
            SqlCommandHelpers.ExecuteSqlCommand(_cfg.ExampleDatabaseConnectionString, script);

        private void TryCreateDatabase()
        {
            try
            {
                SqlCommandHelpers.ExecuteSqlCommand(_cfg.MasterConnectionString,
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

    public class DatabaseDirectoryManager
    {
        private readonly DatabaseConfig _cfg;

        public DatabaseDirectoryManager(DatabaseConfig databaseConfig)
        {
            _cfg = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
        }

        public bool DoesDatabaseDirectoryExists()
        {
            var dbFileDirectory = GetDirectoryOfFile(_cfg.DatabasePath);
            return dbFileDirectory.Exists;
        }

        // TODO This can be moved to general util helper class
        private static DirectoryInfo GetDirectoryOfFile(string path)
        {
            var dbFileInfo = new FileInfo(path);
            var dbFileDirectory = dbFileInfo.Directory;
            if (dbFileDirectory == null)
                throw new InvalidOperationException($"{nameof(dbFileDirectory)} is null!");

            return dbFileDirectory;
        }

        public void TryCreateDatabaseDirectory()
        {
            var dbDirectory = GetDirectoryOfFile(_cfg.DatabasePath);

            TryCreateDirectory(dbDirectory);
        }

        private static void TryCreateDirectory(DirectoryInfo directory)
        {
            var parentDir = directory.Parent;
            if (parentDir != null && !parentDir.Exists)
                TryCreateDirectory(parentDir); // Recursion!

            directory.Create();
        }
    }
}