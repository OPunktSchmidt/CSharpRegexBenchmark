using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace CSharpRegexBenchmark.Benchmarks
{
    /// <summary>
    /// Base class for a benchmark class
    /// </summary>
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(BenchmarkDotNet.Mathematics.NumeralSystem.Arabic)]
    public class AbsBenchmarkCase
    {
        protected const string RegExPattern = @"(\b[\w]+(['\-]\w+)?\b)(?=.*\1)";
    }
}
