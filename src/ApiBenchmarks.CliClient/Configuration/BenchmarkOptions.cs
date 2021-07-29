// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.Configuration
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Options for benchmark configuration.
    /// </summary>
    public class BenchmarkOptions
    {
        /// <summary>
        /// Section name in the configuration file.
        /// </summary>
        public const string SectionName = "Benchmarks";

        static BenchmarkOptions()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            Instances = config.GetSection(SectionName).Get<List<BenchmarkOptions>>();
        }

        /// <summary>
        /// Gets or sets the configuration instances.
        /// </summary>
        public static IList<BenchmarkOptions> Instances { get; set; }

        /// <summary>
        /// Gets or sets the name of this benchmark option. Quick, Full, etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the launch count. This is the # of executions of each benchmark test
        /// performed.
        /// </summary>
        public int LaunchCount { get; set; }

        /// <summary>
        /// Gets or sets the iteration count. This is the # of iterations performed of each
        /// benchmark test. The total # of executions performed for any given benchmark operation
        /// is IterationCount * LaunchCount * InvocationCount.
        /// </summary>
        public int IterationCount { get; set; }

        /// <summary>
        /// Gets or sets the invocation count. This is the # of executions of each benchmark test
        /// performed within an iteration.
        /// </summary>
        public int InvocationCount { get; set; }

        /// <summary>
        /// Gets or sets the warmup count. # of ops of a benchmark performed to warm things up prior to actual
        /// test.
        /// </summary>
        public int WarmupCount { get; set; }

        /// <summary>
        /// Gets or sets the unroll factor. Not entirely sure what this is TBH.
        /// </summary>
        public int UnrollFactor { get; set; }
    }
}
