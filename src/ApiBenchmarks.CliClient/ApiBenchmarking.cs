// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System.IO;

    using ApiBenchmarks.Grpc;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Jobs;

    using global::Grpc.Net.Client;

    using static ApiBenchmarks.Grpc.Benchmarker;

    /// <summary>
    /// Performs benchmark tests.
    /// </summary>
    [EventPipeProfiler(EventPipeProfile.CpuSampling)]
    [Config(typeof(BenchmarkConfig))]
    [SimpleJob(RuntimeMoniker.Net50, launchCount: 3, warmupCount: 10, targetCount: 30, invocationCount: 20)]
    public class ApiBenchmarking
    {
        /// <summary>
        /// Gets the full filename and path for our grpc parameters.
        /// </summary>
        public static string GrpcParametersFilePath => Path.Combine(Path.GetTempPath(), "grpcparams.txt");

        private SimpleRequest Request { get; set; }

        private BenchmarkerClient Client { get; set; }

        /// <summary>
        /// Global setup, happens once at the start of benchmarking.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            var requestGeneratorParameters = RequestGeneratorParameters.ReadFromFile();
            var generator = new GrpcRequestGenerator(requestGeneratorParameters);
            var grpcPort = int.Parse(File.ReadAllText(GrpcParametersFilePath));
            var channel = GrpcChannel.ForAddress($"https://localhost:{grpcPort}");
            this.Client = new BenchmarkerClient(channel);
            this.Request = generator.Generate();
        }

        /// <summary>
        /// GRPC+Protobuf benchmark.
        /// </summary>
        [Benchmark(Baseline = true, OperationsPerInvoke = 4, Description = "GRPC+Protobuf")]
        public void GrpcPlusProtobuf()
        {
            this.Client.Simple(this.Request);
        }
    }
}
