using Ploeh.AutoFixture;
using SampleDatabase.DbContext.Entities;

namespace SampleDatabase.DbContext.Migrations.Configuration
{
    public class SampleDbContextSeeder
    {
        const int generateItemsNumber = 1000;

        public void Seed(SampleDbContext dbContext)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            AddFoos(dbContext, fixture);
            AddBars(dbContext, fixture);
        }

        private static void AddFoos(SampleDbContext migrationConfiguration, Fixture fixture)
        {
            var fooCollection = fixture.CreateMany<Foo>(generateItemsNumber);
            migrationConfiguration.Foos.AddRange(fooCollection);
        }

        private void AddBars(SampleDbContext dbContext, Fixture fixture)
        {
            var barCollection = fixture.CreateMany<Bar>(generateItemsNumber);
            dbContext.Bars.AddRange(barCollection);
        }
    }
}
