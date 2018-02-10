using System;
using PagiNET.IntegrationTests.Utilities;

namespace PagiNET.IntegrationTests.Setup
{
    public class DatabaseDirectoryManager
    {
        private readonly DatabaseConfig _cfg;

        public DatabaseDirectoryManager(DatabaseConfig databaseConfig)
        {
            _cfg = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
        }

        public bool DoesDatabaseDirectoryExists()
        {
            var dbFileDirectory = DirectoryHelper.GetDirectoryOfFile(_cfg.DatabasePath);
            return dbFileDirectory.Exists;
        }

        public void TryCreateDatabaseDirectory()
        {
            var dbDirectory = DirectoryHelper.GetDirectoryOfFile(_cfg.DatabasePath);
            DirectoryHelper.TryCreateDirectory(dbDirectory);
        }
    }
}