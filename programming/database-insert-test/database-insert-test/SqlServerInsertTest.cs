using System;
using System.Data.SqlClient;
using BenchmarkDotNet.Attributes;
using CSVFile;
using Npgsql;
using Postgres2Go;

namespace database_insert_test
{
    public class SqlServerInsertTest
    {
        private SqlConnection? _conn;

        [GlobalSetup]
        public void Setup()
        {
            _conn = new SqlConnection(Environment.GetEnvironmentVariable("SQL_CONN_STR"));
            _conn.Open();

            // Destruct and reconstruct the database if necessary
            using (var cmd = new SqlCommand("CREATE TABLE  version()", _conn))
            {
                var version = cmd.ExecuteScalar() as string;

                Console.WriteLine($"PostgreSQL version: {version}");
            }
        }

        /// <summary>
        /// This benchmark variant inserts all the records using the simplest
        /// possible SQL statement in a loop.
        /// </summary>
        [Benchmark]
        public void InsertSingleLoop()
        {
            using (var cmd = new SqlCommand("CREATE TABLE  version()", _conn))
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

