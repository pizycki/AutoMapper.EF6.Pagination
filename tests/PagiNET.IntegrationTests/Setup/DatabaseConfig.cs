using System;
using PagiNET.IntegrationTests.Config;

namespace PagiNET.IntegrationTests.Setup
{
    public class DatabaseConfig
    {
        private readonly EnvConfig _env;

        public DatabaseConfig(EnvConfig env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public string DatabaseName => _env.DatabaseName;

        public string DatabasePath => _env.DatabasePath;

        /// <summary>
        /// Used when creating new database
        /// </summary>
        public string MasterConnString =>
            ConnectionStringHelper.CreateMasterCatalogConnString(_env.DataSource).ToString();

        /// <summary>
        /// For any query to existing database
        /// </summary>
        public string ExampleDatabaseConnString =>
            ConnectionStringHelper.CreateConnString(_env.DataSource, _env.DatabaseName).ToString();

    }
}