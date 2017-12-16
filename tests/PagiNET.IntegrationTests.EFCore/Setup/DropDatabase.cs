using System.IO;
using System.Linq;
using PagiNET.IntegrationTests.EFCore.SqlCommands;

namespace PagiNET.IntegrationTests.EFCore.Setup
{
    public class DropDatabase
    {
        private readonly DatabaseConfig _cfg;

        public DropDatabase(DatabaseConfig cfg)
        {
            _cfg = cfg;
        }

        public void Go()
        {
            var fileNames = SqlCommandHelpers.ExecuteSqlQuery(_cfg.ConnectionString, $@"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('{_cfg.DatabaseName}')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                SqlCommandHelpers.ExecuteSqlCommand(_cfg.ConnectionString, $@"
                    ALTER DATABASE [{_cfg.DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db '{_cfg.DatabaseName}'");

                fileNames.ForEach(File.Delete);
            }
        }
    }
}