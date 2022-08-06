# CSharpRegexBenchmark

[![Build](https://github.com/OPunktSchmidt/CSharpRegexBenchmark/actions/workflows/build.yaml/badge.svg)](https://github.com/OPunktSchmidt/CSharpRegexBenchmark/actions/workflows/build.yaml)

This small project is used to evaluate the different possibilities of executing a regular expression in C# and to compare the performance.

It uses [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) to generate reliable results. 

## Any Feedback?

If you have ideas how the benchmark can be extended or improved or you have found errors, please open an issue.

## Thoughts on benchmark design

Benchmarking regular expressions is tricky. After all, we want the benchmark to be transferrable to the real world. There are several things to consider:

* In a real application, one would not create a RegEx object with RegexOptions.Compiled just to run a RegEx once. Therefore, it makes sense to benchmark the creation of a RegEx object and the execution speed separately.

* The execution speed of a RegEx depends on many factors. On the one hand from the RegEx pattern itself (complexity) and the length of the strings against which the RegEx is applied. Just because RegexOptions.Compiled results in faster execution speed in many cases, doesn't mean it applies to all RegEx patterns and strings (we'll see such a case later in the test results 😉).

* The execution speed depends on the .Net Framework version. Microsoft has made performance improvements between different framework versions. This benchmark is designed to test all variants (Interpreted, Runtime-Compiled and Buildtime-Compiled Regular Expressions). Buildtime-Compiled Regular Expressions are only available from .Net 7 and therefore this benchmark is run with at least .Net 7. Older .Net versions are not considered.

## What options does .Net (from .Net 7 and C#11) offer for executing regular expressions?

1. Interpreted
```cs
for (int i = 0 ; i < 10; i++)
   {
     // Code to execute.
   }
```
