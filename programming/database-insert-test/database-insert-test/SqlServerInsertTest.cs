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
        public async Task SetupFunc()
        {
            Console.WriteLine("Global setup for SQL");
            var configuration = new MsSqlTestcontainerConfiguration();
            configuration.Password = "yourStrong(!)Password";
            configuration.Port = 9959;
            _sqlserver = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(configuration)
                .WithHostname("172.20.0.1")
                .WithExposedPort(9959)
                .Build();
            await _sqlserver.StartAsync();
            Console.WriteLine("Status is: " + _sqlserver.State.ToString());
            _conn = new SqlConnection(_sqlserver.ConnectionString);
            _conn.Open();

            // Destruct and reconstruct the database if necessary
            using (var cmd = new SqlCommand(CREATE_SQL, _conn))
            {
                await cmd.ExecuteNonQueryAsync();
            }
            Console.WriteLine("Finished global setup for SQL");
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
            Console.WriteLine("Global cleanup for SQL");
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
            if (_sqlserver != null)
            {
                await _sqlserver.StopAsync();
                await _sqlserver.DisposeAsync();
            }
            Console.WriteLine("Finished global cleanup for SQL");
        }
    }
}

