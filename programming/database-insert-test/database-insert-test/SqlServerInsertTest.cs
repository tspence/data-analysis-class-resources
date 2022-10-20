using System.Data.SqlClient;
using BenchmarkDotNet.Attributes;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace database_insert_test
{
    public class SqlServerInsertTest
    {
        private SqlConnection? _conn;
        private TestcontainerDatabase? _sqlserver;

        private static string CREATE_SQL = @"
CREATE TABLE mailing_address (
    line1 varchar(250) NULL,
    city varchar(100) NULL,
    region varchar(50) NULL,
    country varchar(50) NULL,
    postalCode varchar(20) NULL
);";
        private static string INSERT_SQL = @"
INSERT INTO mailing_address (line1, city, region, country, postalCode) VALUES (@line1, @city, @region, @country, @postalCode);";

        [GlobalSetup]
        public void SetupFunc()
        {
            try
            {
                _sqlserver = new TestcontainersBuilder<MsSqlTestcontainer>()
                    .WithDatabase(new MsSqlTestcontainerConfiguration()
                    {
                        Database = "db",
                        Username = "sqlserver",
                        Password = "sqlserver",
                    })
                    .Build();
                _conn = new SqlConnection(_sqlserver.ConnectionString);
                _conn.Open();

                // Destruct and reconstruct the database if necessary
                using (var cmd = new SqlCommand(CREATE_SQL, _conn))
                {
                    var version = cmd.ExecuteNonQuery();

                    Console.WriteLine($"SqlServer version: {version}");
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// This benchmark variant inserts all the records using the simplest
        /// possible SQL statement in a loop.
        /// </summary>
        [Benchmark]
        public async Task InsertSingleLoop()
        {
            using (var cmd = new SqlCommand(INSERT_SQL, _conn))
            {
                foreach (var address in MailingAddress.GetAll())
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@line1", address.Line1 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@city", address.City ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@region", address.Region ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@country", address.Country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@postalCode", address.PostalCode ?? (object)DBNull.Value);
                    var version = await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        [GlobalCleanup]
        public async Task Cleanup()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
            if (_sqlserver != null)
            {
                await _sqlserver.DisposeAsync();
            }
        }
    }
}

