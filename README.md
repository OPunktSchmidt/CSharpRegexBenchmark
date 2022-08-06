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

**1. Interpreted** 

The regular expression is interpreted. This is usually slow.

```cs
var regex = new Regex(@"\w", RegexOptions.CultureInvariant);
regex.Matches("test");
```

**2. Runtime compiled** 

The regular expression is compiled into machine code at runtime. This is usually faster, but it takes a one-off time to compile. Depending on how complex the regular expression is and how often you want it to be executed, interpreted mode can be faster because it eliminates the time it takes to compile.

```cs
var regex = new Regex(@"\w", RegexOptions.Compiled | RegexOptions.CultureInvariant);
regex.Matches("test");
```

**3. Runtime compiled** 

This feature is only available from .Net 7. The regular expression is compiled at build time. On the one hand, this eliminates the need for compilation at runtime. On the other hand, the compiler can theoretically make further optimizations than with RegexOptions.Compiled at runtime. However, in .Net 7, RegexGenerator and RegexOptions.Compiled seem to do the same optimizations, so there's no benefit here. It will be interesting to see how this develops in future .Net releases.

```cs
[RegexGenerator(@"\w", RegexOptions.CultureInvariant)]
private static partial Regex GetBuildtimeCompiledMatchRegex();
```
⚠ Warning ⚠

[RegexGenerator] cannot compile all regular expressions. This is not immediately noticeable and you only notice it when you look at the generated code. RegexGenerator generates C# code that natively implements the regular expression. If the regular expression is not supported, the generated code will just say something like:

```cs
 [GeneratedCodeAttribute("System.Text.RegularExpressions.Generator", "7.0.6.32404")]
 [EditorBrowsable(EditorBrowsableState.Never)]
 internal static class __58a41014
 {
      /// <summary>Caches a <see cref="Regex"/> instance for the GetCompiledRegex method.</summary>
      /// <remarks>A custom Regex-derived type could not be generated because the expression contains case-insensitive backreferences which are not supported by the source generator.</remarks>
      internal sealed class GetCompiledRegex_0 : Regex
      {
          /// <summary>Cached, thread-safe singleton instance.</summary>
          internal static readonly Regex Instance = new("(\\b[\\w]+(['\\-]\\w+)?\\b)(?=.*\\1)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
      }
        
 }
```
