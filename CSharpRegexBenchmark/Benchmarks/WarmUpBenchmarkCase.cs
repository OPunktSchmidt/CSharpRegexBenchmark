using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace CSharpRegexBenchmark.Benchmarks
{
    /// <summary>
    /// Contains benchmarks to test the creation of a RegEx object
    /// </summary>
    public partial class WarumUpBenchmarkCase : AbsBenchmarkCase
    {
        [RegexGenerator(RegExPattern, RegexOptions.CultureInvariant)]
        private static partial Regex GetBuildtimeCompiledWarmUpRegex();

        /// <summary>
        /// Creates a RegEx object with an interpreted RegEx
        /// </summary>
        [Benchmark(Baseline = true)]
        public int[] InterpretedRegex()
        {
            var regex = new Regex(RegExPattern, RegexOptions.CultureInvariant);
            return regex.GetGroupNumbers();
        }

        /// <summary>
        /// Creates a RegEx object with an runtime compiled RegEx
        /// </summary>
        [Benchmark]
        public int[] RuntimeCompiledRegex()
        {
            var regex = new Regex(RegExPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
            return regex.GetGroupNumbers();
        }

        /// <summary>
        /// Creates a RegEx object with an buildtime compiled RegEx
        /// </summary>
        [Benchmark]
        public int[] BuildtimeCompiledRegex()
        {
            var regex = GetBuildtimeCompiledWarmUpRegex();
            return regex.GetGroupNumbers();
        }
    }
}
