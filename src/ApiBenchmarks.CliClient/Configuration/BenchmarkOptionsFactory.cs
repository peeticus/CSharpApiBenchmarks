// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.Configuration
{
    using System;
    using System.Linq;

    /// <summary>
    /// Factory class for creating/reading instances of <see cref="BenchmarkOptions"/>.
    /// </summary>
    public class BenchmarkOptionsFactory
    {
        /// <summary>
        /// Creates a <see cref="BenchmarkOptions"/> instance.
        /// </summary>
        /// <param name="quickBenchmark">True if this is a quick benchmark, otherwise false.</param>
        /// <returns>Matching benchmark options.</returns>
        public BenchmarkOptions Create(bool quickBenchmark)
        {
            return quickBenchmark ? this.Create("quick") : this.Create("full");
        }

        /// <summary>
        /// Creates a <see cref="BenchmarkOptions"/> instance.
        /// </summary>
        /// <param name="benchmarkOptionName">The name of the options instance to create.</param>
        /// <returns>Matching benchmark options.</returns>
        protected BenchmarkOptions Create(string benchmarkOptionName)
        {
            return BenchmarkOptions.Instances.First(option => option.Name.Equals(benchmarkOptionName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
