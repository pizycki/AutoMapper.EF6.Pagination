using System;
using ExampleDbContext;
using PagiNET.IntegrationTests.Setup;

namespace PagiNET.IntegrationTests.Fixtures
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RealDatabaseTestFixture : IDisposable
    {
        private ITestDatabaseManager DbManager { get; } = new TestDatabaseManager();

        public RealDatabaseTestFixture()
        {
            DbManager.CreateDatabase();
        }

        public void Dispose()
        {
            DbManager.DropDatabase();
        }

        public Context CreateContext() => DbManager.CreateDbContext();
    }
}