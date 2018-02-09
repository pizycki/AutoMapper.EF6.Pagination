using System;
using System.Threading.Tasks;
using ExampleDbContext;
using PagiNET.IntegrationTests.Setup;

namespace PagiNET.IntegrationTests.Fixtures
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RealDatabaseTestFixture : IDisposable
    {
        private ITestDatabaseManager DbManager { get; } = new TestDatabaseManager();

        public RealDatabaseTestFixture() => DbManager.CreateDatabase();

        public void Dispose() => DbManager.DropDatabase();

        public Context CreateContext() => DbManager.CreateDbContext();
    }

    public static class RealDatabaseTestFixtureExtensions
    {
        public static T Query<T>(this RealDatabaseTestFixture fixture, Func<Context, T> query)
        {
            if (fixture == null)
                throw new ArgumentNullException(nameof(fixture));

            using (var ctx = fixture.CreateContext())
                return query(ctx);
        }

        public static async Task<T> QueryAsync<T>(this RealDatabaseTestFixture fixture, Func<Context, Task<T>> query)
        {
            if (fixture == null)
                throw new ArgumentNullException(nameof(fixture));

            using (var ctx = fixture.CreateContext())
                return await query(ctx);
        }
    }
}