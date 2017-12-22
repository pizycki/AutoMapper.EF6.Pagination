using ExampleDbContext;
using Microsoft.EntityFrameworkCore;

namespace PagiNET.IntegrationTests.Setup
{
    public class DbContextProvider
    {
        private readonly DatabaseConfig _databaseConfig;

        public DbContextProvider(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public Context CreateDbContext() => new Context(BuildDbContextOptions());

        private DbContextOptions<Context> BuildDbContextOptions() =>
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(_databaseConfig.ConnectionString)
                .Options;
    }
}