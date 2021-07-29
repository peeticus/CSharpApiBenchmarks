// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    /// <summary>
    /// Benchmark completion event arguments.
    /// </summary>
    public class BenchmarkCompleteEventArgs
    {
        /// <summary>
        /// Gets or sets the name of the benchmark completed.
        /// </summary>
        public string BenchmarkName { get; set; }

        /// <summary>
        /// Gets or sets the benchmark results.
        /// </summary>
        public (int RequestSize, int ResponseSize) BenchmarkResults { get; set; }
    }
}
