// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.Configuration
{
    using ApiBenchmarks.CliClient.OutputColumns;

    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Exporters.Csv;
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
            var jobOptions = new BenchmarkOptionsFactory().Create(parameters.ShortRun);

            this.AddJob(
                Job.ShortRun
                    .WithLaunchCount(jobOptions.LaunchCount)
                    .WithIterationCount(jobOptions.IterationCount)
                    .WithInvocationCount(jobOptions.InvocationCount)
                    .WithWarmupCount(jobOptions.WarmupCount)
                    .WithUnrollFactor(jobOptions.UnrollFactor));

            this.AddDiagnoser(MemoryDiagnoser.Default);
            this.AddExporter(CsvMeasurementsExporter.Default);
            this.AddExporter(CsvExporter.Default);
            this.AddExporter(HtmlExporter.Default);

            if (new WindowsRightsChecker().IsElevated())
            {
                this.AddHardwareCounters(HardwareCounter.TotalCycles);
            }

            this.AddDiagnoser(MemoryDiagnoser.Default);
            this.AddLogicalGroupRules(BenchmarkLogicalGroupRule.ByCategory);
            this.AddColumn(
                    StatisticColumn.P95,
                    CategoriesColumn.Default,
                    StatisticColumn.OperationsPerSecond);

            this.AddColumn(new BytesSentColumn());
            this.AddColumn(new BytesReceivedColumn());
        }
    }
}
