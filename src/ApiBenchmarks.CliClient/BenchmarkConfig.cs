// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Jobs;

    /// <summary>
    /// Configuration setup for the benchmarking run.
    /// </summary>
    public class BenchmarkConfig : ManualConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkConfig"/> class.
        /// </summary>
        public BenchmarkConfig()
        {
            var parameters = BenchmarkParameters.ReadFromFile();

            if (parameters.ShortRun)
            {
                this.AddJob(
                    Job.ShortRun
                        .WithLaunchCount(3)
                        .WithIterationCount(3)
                        .WithInvocationCount(20)
                        .WithWarmupCount(2)
                        .WithUnrollFactor(5));
            }
            else
            {
                this.AddJob(
                    Job.ShortRun
                        .WithLaunchCount(10)
                        .WithIterationCount(10)
                        .WithInvocationCount(100)
                        .WithWarmupCount(5)
                        .WithUnrollFactor(10));
            }

            this.AddDiagnoser(MemoryDiagnoser.Default);
            this.AddColumn(
                    StatisticColumn.P90,
                    StatisticColumn.P95,
                    CategoriesColumn.Default,
                    StatisticColumn.OperationsPerSecond);
        }
    }
}
