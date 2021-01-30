using BenchmarkDotNet.Running;
using System;

namespace ObjectDiffFinder.BenchmarkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Object diff method benchmarks!");
            var summary = BenchmarkRunner.Run<VerifyBenchmarks>();

            Console.ReadKey();
        }
    }
}
