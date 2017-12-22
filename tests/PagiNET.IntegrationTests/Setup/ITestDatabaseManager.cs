using ExampleDbContext;

namespace PagiNET.IntegrationTests.Setup
{
    public interface ITestDatabaseManager
    {
        void CreateDatabase();
        void DropDatabase();
        Context CreateDbContext();
    }
}