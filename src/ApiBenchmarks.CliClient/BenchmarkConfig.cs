// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;

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
            this.AddColumn(
                    StatisticColumn.P80,
                    StatisticColumn.P90,
                    StatisticColumn.P95,
                    StatisticColumn.P100);
        }
    }
}
