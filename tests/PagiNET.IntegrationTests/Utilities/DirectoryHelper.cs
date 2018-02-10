using System;
using System.IO;

namespace PagiNET.IntegrationTests.Utilities
{
    public static class DirectoryHelper
    {
        public static DirectoryInfo GetDirectoryOfFile(string path)
        {
            var dbFileInfo = new FileInfo(path);
            var dbFileDirectory = dbFileInfo.Directory;
            if (dbFileDirectory == null)
                throw new InvalidOperationException($"{nameof(dbFileDirectory)} is null!");

            return dbFileDirectory;
        }

        public static void TryCreateDirectory(DirectoryInfo directory)
        {
            var parentDir = directory.Parent;
            if (parentDir != null && !parentDir.Exists)
                TryCreateDirectory(parentDir); // Recursion!

            directory.Create();
        }
    }
}
