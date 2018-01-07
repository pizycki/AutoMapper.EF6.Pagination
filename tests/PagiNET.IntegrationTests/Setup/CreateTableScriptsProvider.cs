using System.IO;
using System.Linq;
using System.Reflection;
using ExampleDbContext;

namespace PagiNET.IntegrationTests.Setup
{
    internal class CreateTableScriptsProvider
    {
        private readonly string[] ResourceNames = {
            "ExampleDbContext.Scripts.CreateTables.sql"
        };

        public string[] GetScripts() => ResourceNames.Select(ReadEmbeddedResource).ToArray();

        public string ReadEmbeddedResource(string resourceName)
        {
            var resourceStream = Assembly.GetAssembly(typeof(Context)).GetManifestResourceStream(resourceName);
            if (resourceStream == null)
                return null;

            using (var reader = new StreamReader(resourceStream))
                return reader.ReadToEnd();
        }
    }
}