using System;

namespace PagiNET.IntegrationTests.Config
{
    /// <summary>
    /// Environment dependant configuration.
    /// Configuration may vary depending on when test are being ran.
    /// </summary>
    public class EnvConfig
    {
        /// <summary>
        /// Static constructor 
        /// Loads up configuration with <see cref="DotNetEnv.Env"/>.
        /// </summary>
        static EnvConfig() => DotNetEnv.Env.Load();

        /// <summary>
        /// Name of database visible by MSSQL instance
        /// </summary>
        public string DatabaseName => GetEnvVariable("DATABASE_NAME");

        /// <summary>
        /// Connection string data source
        /// </summary>
        public string DataSource => GetEnvVariable("DATABASE_DATASOURCE"); // @"(LocalDB)\MSSQLLocalDB"

        /// <summary>
        /// Local path to database .mdf file.
        /// </summary>
        public string DatabasePath =>
            TryGetAppVeyorDatabasePath()
            ?? GetEnvVariable("PAGINET_DATABASE_PATH")
            ?? GetDefaultDatabasePath;

        private static string TryGetAppVeyorDatabasePath() =>
            IsAppVeyor
                ? GetEnvVariable("APPVEYOR_BUILD_FOLDER") + "\\" + DatabaseFileName
                : null;

        private static bool IsAppVeyor => bool.TryParse(GetEnvVariable("APPVEYOR"), out var isAppVeyor) && isAppVeyor;

        private static string DatabaseFileName => "PagiNET_IntegrationTest_Database.mdf";

        private static string GetDefaultDatabasePath => GetUserProfileDirectoryPath + "\\" + DatabaseFileName;

        private static string GetUserProfileDirectoryPath => Environment.ExpandEnvironmentVariables($@"%USERPROFILE%\{DatabaseFileName}");

        private static string GetEnvVariable(string variableName) => Environment.GetEnvironmentVariable(variableName);

    }
}
