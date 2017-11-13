using NUnit.Framework;
using RealDatabaseTests.Setup;

namespace RealDatabaseTests.Tests
{
    [TestFixture]
    public class Tiesto
    {
        private TestDatabaseManager DbManager { get; } = TestDatabaseManager.CreateFromScratch();

        [OneTimeSetUp]
        public void Setup()
        {
            DbManager.SetUpDatabase();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            DbManager.TearDownDatabase();
        }

        [Test]
        public void Bla()
        {

        }
    }
}
