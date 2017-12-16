using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PagiNET.IntegrationTests.EFCore.SqlCommands
{
    public static class SqlCommandHelpers
    {
        public static void ExecuteSqlCommand(string connectionString, string commandText)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<T> ExecuteSqlQuery<T>(
            string connectionString,
            string queryText,
            Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }
    }
}
