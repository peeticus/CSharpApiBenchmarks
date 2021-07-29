// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.Configuration
{
    using System.IO;

    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Configuration section used for output.
    /// </summary>
    internal class OutputOptions
    {
        /// <summary>
        /// Name of the configuration file section.
        /// </summary>
        public const string SectionName = "Output";

        static OutputOptions()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            Instance = config.GetSection(SectionName).Get<OutputOptions>();
        }

        /// <summary>
        /// Gets or sets the singleton instance of this class.
        /// </summary>
        public static OutputOptions Instance { get; set; }

        /// <summary>
        /// Gets or sets the temp folder used to store benchmarking information.
        /// </summary>
        public string BenchmarksTempFolder { get; set; }
    }
}
