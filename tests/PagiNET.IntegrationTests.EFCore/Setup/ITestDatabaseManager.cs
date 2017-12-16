using ExampleDbContext;

namespace PagiNET.IntegrationTests.EFCore.Setup
{
    public interface ITestDatabaseManager
    {
        void CreateDatabase();
        void DropDatabase();
        Context CreateDbContext();
    }
}