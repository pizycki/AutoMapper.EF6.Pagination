using System.Data.SqlClient;

namespace RealDatabaseTests.Setup
{
    public class TestDatabaseManager
    {
        public static TestDatabaseManager CreateFromScratch() => new TestDatabaseManager();

        public CreateAndDropDatabase CreateAndDropDatabase { get; }

        private TestDatabaseManager()
        {
            CreateAndDropDatabase = new CreateAndDropDatabase(
                new CreateAndDropDatabase.Config(MasterConnString, DatabaseName, DatabaseFileName));
        }

        public void SetUpDatabase()
        {
            CreateAndDropDatabase.DestroyDatabase();
            CreateAndDropDatabase.CreateDatabase();
        }

        public void TearDownDatabase()
        {
            CreateAndDropDatabase.DestroyDatabase();
        }

        private static string DatabaseName = "SampleDatabase";
        private static string DatabaseFileName => @"C:\Users\pizycki\SampleDatabase.mdf"; // TODO Change with configuration
        private static string MasterConnString => Master.ToString();

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "Master",
                IntegratedSecurity = true
            };
    }
}
