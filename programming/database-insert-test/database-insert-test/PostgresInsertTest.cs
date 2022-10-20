using System;
using BenchmarkDotNet.Attributes;
using CSVFile;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Npgsql;
using Utilities;

namespace database_insert_test
{
    public class PostgresInsertTest
    {
        private NpgsqlConnection? _conn;

        private readonly TestcontainerDatabase _postgres = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "db",
                Username = "postgres",
                Password = "postgres",
            })
            .Build();

        [GlobalSetup]
        public void Setup()
        {
            _conn = new NpgsqlConnection(_postgres.ConnectionString);
            _conn.Open();
            using (var cmd = new NpgsqlCommand("CREATE TABLE address ()", _conn))
            {
                var version = cmd.ExecuteScalar() as string;

                Console.WriteLine($"PostgreSQL version: {version}");
            }

            // Read in the CSV addresses
        }

        /// <summary>
        /// This benchmark variant inserts all the records using the simplest
        /// possible SQL statement in a loop.
        /// </summary>
        [Benchmark]
        public void InsertSingleLoop()
        {
            using (var cmd = new NpgsqlCommand("CREATE TABLE  version()", _conn))
            {
                var version = cmd.ExecuteScalar() as string;

                Console.WriteLine($"PostgreSQL version: {version}");
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
        }
    }
}

