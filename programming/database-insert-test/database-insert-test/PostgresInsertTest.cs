using System;
using BenchmarkDotNet.Attributes;
using CSVFile;
using Npgsql;
using Postgres2Go;
using Utilities;

namespace database_insert_test
{
    public class PostgresInsertTest
    {
        private PostgresRunner? _runner;
        private NpgsqlConnection? _conn;
        private MailingAddress[]? _addresses;


        [GlobalSetup]
        public void Setup()
        {
            _runner = PostgresRunner.Start();
            _conn = new NpgsqlConnection(_runner.GetConnectionString());
            _conn.Open();
            using (var cmd = new NpgsqlCommand("CREATE TABLE  version()", _conn))
            {
                var version = cmd.ExecuteScalar() as string;

                Console.WriteLine($"PostgreSQL version: {version}");
            }

            // Read in the CSV addresses
            var text = File.ReadAllText("starbucks_addresses.csv");
            _addresses = CSV.Deserialize<MailingAddress>(text).ToArray();
            Console.WriteLine($"Read in {_addresses.Length} addresses");
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
            if (_runner != null)
            {
                _runner.Dispose();
            }
        }
    }
}

