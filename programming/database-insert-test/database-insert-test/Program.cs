using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace database_insert_test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .AddLogger(new ConsoleLogger());
            var summary = BenchmarkRunner.Run<SqlServerInsertTest>(config);
            Console.WriteLine($"Summary: {summary}");
        }
    }
}