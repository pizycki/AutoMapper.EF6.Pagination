namespace PagiNET.IntegrationTests.EFCore.Setup
{
    public class DatabaseConfig
    {
        public DatabaseConfig(
            string connectionString,
            string databaseName,
            string databaseFileName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            DatabaseFileName = databaseFileName;
        }

        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string DatabaseFileName { get; }
    }
}