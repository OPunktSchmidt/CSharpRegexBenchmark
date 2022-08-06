
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpRegexBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                 {
                     services.AddHostedService<ApplicationWorker>();
                 })
                 .Build();

            host.Run();
        }
    }
}