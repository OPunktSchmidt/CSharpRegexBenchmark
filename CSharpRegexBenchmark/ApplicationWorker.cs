using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BenchmarkDotNet.Running;

namespace CSharpRegexBenchmark
{
    /// <summary>
    /// Runs the program and starts the benchmark
    /// </summary>
    internal class ApplicationWorker : IHostedService
    {
        ILogger<ApplicationWorker> _logger;

        public ApplicationWorker(ILogger<ApplicationWorker> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application started");

            await Task.Run(() =>
            {
                BenchmarkRunner.Run<BenchmarkCase>();
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
