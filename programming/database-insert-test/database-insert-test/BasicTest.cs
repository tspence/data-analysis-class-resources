using System;
using System.Data.SqlClient;
using BenchmarkDotNet.Attributes;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace database_insert_test
{
    public class BasicTest
    {
        [Benchmark]
        public void TestSomething()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Test");
            }
        }

        public static async Task InitContainerTest()
        {
            var testcontainersBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithHostname("tcp://127.0.0.1:2375")
                .WithDatabase(new MsSqlTestcontainerConfiguration
                {
                    Password = "yourStrong(!)Password123",
                });

            await using (var testcontainer = testcontainersBuilder.Build())
            {
                Console.WriteLine("Build succeeded");
                await testcontainer.StartAsync();
                Console.WriteLine("Build succeeded");

                await using (var connection = new SqlConnection(testcontainer.ConnectionString))
                {
                    connection.Open();

                    await using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT 1";
                        var rs = await cmd.ExecuteReaderAsync();
                        Console.WriteLine("Result is: " + rs.GetInt32(0).ToString());
                    }
                }
            }
        }
    }
}

