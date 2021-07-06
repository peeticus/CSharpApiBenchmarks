// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System.IO;
    using System.Text.Json;

    /// <summary>
    /// Parameters for the benchmark test.
    /// </summary>
    public class BenchmarkParameters
    {
        /// <summary>
        /// Gets or sets GRPC server port.
        /// </summary>
        public int GrpcPort { get; set; }

        /// <summary>
        /// Gets or sets REST server port.
        /// </summary>
        public int RestPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to do a short run. If false, a long run is done.
        /// </summary>
        public bool ShortRun { get; set; }

        /// <summary>
        /// Reads an object of this type from a file in the temp directory.
        /// </summary>
        /// <returns>Populated instance from the file.</returns>
        public static BenchmarkParameters ReadFromFile()
        {
            var configContents = File.ReadAllText(Path.Combine(Path.GetTempPath(), $"{nameof(BenchmarkParameters)}.txt"));
            return JsonSerializer.Deserialize<BenchmarkParameters>(configContents);
        }

        /// <summary>
        /// Saves this object to a file in the temp directory.
        /// </summary>
        public void SaveToFile()
        {
            File.WriteAllText(Path.Combine(Path.GetTempPath(), $"{nameof(BenchmarkParameters)}.txt"), JsonSerializer.Serialize(this));
        }
    }
}
