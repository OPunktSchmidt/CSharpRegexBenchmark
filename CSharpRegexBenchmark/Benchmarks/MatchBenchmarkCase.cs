using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace CSharpRegexBenchmark.Benchmarks
{
    /// <summary>
    /// Contains benchmarks to test the execution of a RegEx
    /// </summary>
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(BenchmarkDotNet.Mathematics.NumeralSystem.Arabic)]
    public partial class MatchBenchmarkCase : AbsBenchmarkCase
    {
        [RegexGenerator(RegExPattern, RegexOptions.CultureInvariant)]
        private static partial Regex GetBuildtimeCompiledMatchRegex();

        private string _testText;
        private Regex _interpretedRegEx;
        private Regex _runtimeCompiledRegEx;
        private Regex _buildtimeCompiledRegEx;

        /// <summary>
        /// Specifies how long the text should be against which the RegEx is executed.
        /// </summary>
        [Params(10, 100, 1000, 10000, 100000)]
        public int NumberOfWordsInText { get; set; }
       
        [GlobalSetup]
        public void Setup()
        {
            _testText = TextGenerator.GenerateText(NumberOfWordsInText, 3);
            _interpretedRegEx = new Regex(RegExPattern, RegexOptions.CultureInvariant);
            _runtimeCompiledRegEx = new Regex(RegExPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
            _buildtimeCompiledRegEx = GetBuildtimeCompiledMatchRegex();
        }

        /// <summary>
        /// Executes a RegEx which is interpreted
        /// </summary>
        [Benchmark(Baseline = true)]
        public int InterpretedRegex()
        {
            return _interpretedRegEx.Matches(_testText).Count;
        }

        /// <summary>
        /// Executes a RegEx which is compiled at runtime
        /// </summary>
        [Benchmark]
        public int RuntimeCompiledRegex()
        {
            return _runtimeCompiledRegEx.Matches(_testText).Count;
        }

        /// <summary>
        /// Executes a RegEx which is compiled at buildtime
        /// </summary>
        [Benchmark]
        public int BuildtimeCompiledRegex()
        {
            return _buildtimeCompiledRegEx.Matches(_testText).Count;
        }
    }
}
