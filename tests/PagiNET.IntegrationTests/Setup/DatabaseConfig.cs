namespace PagiNET.IntegrationTests.Setup
{
    public class DatabaseConfig
    {
        public DatabaseConfig(
            string masterConnectionString,
            string exampleDatabaseConnectionString,
            string databaseName,
            string databaseFileName)
        {
            MasterConnectionString = masterConnectionString;
            ExampleDatabaseConnectionString = exampleDatabaseConnectionString;
            DatabaseName = databaseName;
            DatabaseFileName = databaseFileName;
        }

        /// <summary>
        /// Used when creating new database
        /// </summary>
        public string MasterConnectionString { get; }

        /// <summary>
        /// For any query to existing database
        /// </summary>
        public string ExampleDatabaseConnectionString { get; }
        public string DatabaseName { get; }
        public string DatabaseFileName { get; }
    }
}