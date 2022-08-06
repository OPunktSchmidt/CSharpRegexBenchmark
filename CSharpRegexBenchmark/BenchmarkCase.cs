using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace CSharpRegexBenchmark
{
    /// <summary>
    /// Contains all benchmark tests and configurations for running them
    /// </summary>
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(BenchmarkDotNet.Mathematics.NumeralSystem.Arabic)]
    public partial class BenchmarkCase
    {
        [RegexGenerator(RegExPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
        private static partial Regex GetCompiledRegex();

        private const string RegExPattern = @"(\b[\w]+(['\-]\w+)?\b)(?=.*\1)";
        private string _testText;

        /// <summary>
        /// Specifies how long the text should be against which the RegEx is executed.
        /// </summary>
        [Params(10, 100, 1000, 100000)]
        public int TestTextLength { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _testText = TextGenerator.GenerateText(TestTextLength, 3);
        }

        /// <summary>
        /// Executes a RegEx which is interpreted
        /// </summary>
        [Benchmark(Baseline = true)]
        public int InterpretedRegex()
        {
            Regex regex = new Regex(RegExPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            return regex.Matches(_testText).Count;
        }

        /// <summary>
        /// Executes a RegEx which is compiled at runtime
        /// </summary>
        [Benchmark]
        public int RuntimeCompiledRegex()
        {
            Regex regex = new Regex(RegExPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            return regex.Matches(_testText).Count;
        }

        /// <summary>
        /// Executes a RegEx which is compiled at buildtime
        /// </summary>
        [Benchmark]
        public int BuildtimeCompiledRegex()
        {
            Regex regex = GetCompiledRegex();
            return regex.Matches(_testText).Count;
        }
    }
}
