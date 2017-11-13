using NUnit.Framework;
using RealDatabaseTests.Setup;

namespace RealDatabaseTests.Tests
{
    [TestFixture]
    public class Tiesto
    {
        private TestDatabaseManager DbManager { get; } = TestDatabaseManager.Create();

        [OneTimeSetUp]
        public void Setup() => DbManager.CreateDatabaseFromScratch();

        [OneTimeTearDown]
        public void Teardown() => DbManager.DropDatabase();

        [Test]
        public void Bla()
        {

        }
    }
}
