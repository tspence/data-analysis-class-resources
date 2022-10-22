using System;
using System.Data.SqlClient;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace database_insert_test
{
    public class Program
    {
       

        public static void Main(string[] args)
        {
            try
            {
                BasicTest.InitContainerTest().Wait();
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
/*
            var config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .AddLogger(new ConsoleLogger());
            var summary = BenchmarkRunner.Run<SqlServerInsertTest>(config);
            Console.WriteLine($"Summary: {summary}");
*/  
        }
    }
}