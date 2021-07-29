// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using ApiBenchmarks.CliClient.Configuration;

    using Newtonsoft.Json;

    using Serilog;

    /// <summary>
    /// Performs benchmarks for bandwidth testing of APIs.
    /// </summary>
    public class BandwidthBenchmarking
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BandwidthBenchmarking"/> class.
        /// </summary>
        /// <param name="benchmarkParameters">Parameters for the benchmark testing.</param>
        public BandwidthBenchmarking(BenchmarkParameters benchmarkParameters)
        {
            this.BenchmarkParameters = benchmarkParameters ?? throw new ArgumentNullException($"{nameof(benchmarkParameters)}");
        }

        /// <summary>
        /// Gets benchmark parameters.
        /// </summary>
        protected BenchmarkParameters BenchmarkParameters { get; }

        /// <summary>
        /// Saves the bandwidth test results for a single specified benchmark (just 1 iteration) to a file.
        /// </summary>
        /// <param name="benchmarkName">The name of the benchmark.</param>
        /// <param name="bandwidth">The bandwidth used.</param>
        /// <returns>Task.</returns>
        public static async Task SaveToFile(string benchmarkName, (int RequestSize, int ResponseSize) bandwidth)
        {
            try
            {
                IList<(int RequestSize, int ResponseSize)> allResults = new List<(int RequestSize, int ResponseSize)>();
                if (File.Exists(GetFileName(benchmarkName)))
                {
                    allResults = await LoadFromFile(benchmarkName);
                }

                allResults.Add(bandwidth);
                await File.WriteAllTextAsync(GetFileName(benchmarkName), JsonConvert.SerializeObject(allResults));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!!" + ex.Message);
            }
        }

        /// <summary>
        /// Loads bandwidth tests for a single specified benchmark from a file.
        /// </summary>
        /// <param name="benchmarkName">The name of the benchmark.</param>
        /// <returns>Benchmark results.</returns>
        public static async Task<IList<(int RequestSize, int ResponseSize)>> LoadFromFile(string benchmarkName)
        {
            var fileContents = await File.ReadAllTextAsync(GetFileName(benchmarkName));
            return JsonConvert.DeserializeObject<IList<(int RequestSize, int ResponseSize)>>(fileContents);
        }

        /// <summary>
        /// Performs a bandwidth test for a single benchmark, by name.
        /// </summary>
        /// <param name="benchmarkName">Name of the benchmark being performed.</param>
        /// <returns>Request size and Response size, in bytes. The bandwidth used.</returns>
        public async Task<(int RequestSize, int ResponseSize)> BandwidthTest(string benchmarkName)
        {
            var benchmarker = new ApiBenchmarking() { ShouldCalculateBandwidth = true };
            benchmarker.GlobalSetup();
            var methodInfo = typeof(ApiBenchmarking).GetMethod(benchmarkName);
            var benchmarkOptions = new BenchmarkOptionsFactory().Create(this.BenchmarkParameters.ShortRun);
            (int RequestSize, int ResponseSize) result = (0, 0);

            for (var launchCount = 1; launchCount <= benchmarkOptions.LaunchCount; launchCount++)
            {
                for (var iterationCount = 1; iterationCount <= benchmarkOptions.IterationCount; iterationCount++)
                {
                    Parallel.For(1, benchmarkOptions.InvocationCount, (invocationIndex) =>
                    {
                        benchmarker.IterationSetup();
                        var task = (Task)methodInfo.Invoke(benchmarker, null);
                        task.Wait();
                        var resultProperty = task.GetType().GetProperty("Result");
                        var (requestSize, responseSize) = ((int RequestSize, int ResponseSize))resultProperty.GetValue(task);
                        result.RequestSize += requestSize;
                        result.ResponseSize += responseSize;
                    });
                }
            }

            var executionCount = benchmarkOptions.LaunchCount * benchmarkOptions.IterationCount * benchmarkOptions.InvocationCount;
            result.RequestSize /= executionCount;
            result.ResponseSize /= executionCount;
            Log.Information($"Bandwidth test complete for {benchmarkName}. Request Size: {result.RequestSize}. Response Size: {result.ResponseSize}");
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Calculates and returns the file name used to store bandwidth benchmark results.
        /// </summary>
        /// <param name="benchmarkName">The name of the benchmark.</param>
        /// <returns>The file name including full path.</returns>
        private static string GetFileName(string benchmarkName)
        {
            return Path.Combine(Path.GetTempPath(), "benchmarks", $"{benchmarkName}.txt");
        }
    }
}
