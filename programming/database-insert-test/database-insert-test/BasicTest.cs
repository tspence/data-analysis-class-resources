using System;
using BenchmarkDotNet.Attributes;

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
    }
}

