// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ApiBenchmarks.CliClient.Configuration;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;

    using global::Grpc.Net.Client;

    using static ApiBenchmarks.Grpc.Benchmarker;

    /// <summary>
    /// Performs benchmark tests.
    /// </summary>
    [EventPipeProfiler(EventPipeProfile.CpuSampling)]
    [Config(typeof(BenchmarkConfig))]
    public class ApiBenchmarking
    {
        private const int ListSizeSmall = 1;
        private const int ListSizeMedium = 5;
        private const int ListSizeLarge = 20;
        private static readonly (int RequestSize, int ResponseSize) EmptyBandwidthResult = new (0, 0);

        /// <summary>
        /// Finalizes an instance of the <see cref="ApiBenchmarking"/> class.
        /// </summary>
        ~ApiBenchmarking()
        {
            this.HttpClient?.Dispose();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the size of requests and responses be calculated.
        /// </summary>
        public bool ShouldCalculateBandwidth { get; set; }

        private GrpcChannel GrpcChannel { get; set; }

        private BenchmarkerClient GrpcClient { get; set; }

        private Grpc.SimpleRequest GrpcRequest { get; set; }

        private Grpc.SimpleRequestList GrpcRequestListSmall { get; set; }

        private Grpc.SimpleRequestList GrpcRequestListMedium { get; set; }

        private Grpc.SimpleRequestList GrpcRequestListLarge { get; set; }

        private Rest.RestClient RestClient { get; set; }

        private HttpClient HttpClient { get; set; }

        private Rest.SimpleRequest RestRequest { get; set; }

        private IList<Rest.SimpleRequest> RestRequestListSmall { get; set; }

        private IList<Rest.SimpleRequest> RestRequestListMedium { get; set; }

        private IList<Rest.SimpleRequest> RestRequestListLarge { get; set; }

        private RequestGeneratorParameters RequestGeneratorParameters { get; set; }

        private BenchmarkParameters BenchmarkParameters { get; set; }

        private Google.Protobuf.WellKnownTypes.Empty GrpcEmpty { get; set; }

        /// <summary>
        /// Clears out the local results folder.
        /// </summary>
        /// <returns>A task.</returns>
        public async Task ClearLocalResultsFolder()
        {
            await Task.Run(() =>
            {
                var directory = Path.Combine(Path.GetTempPath(), OutputOptions.Instance.BenchmarksTempFolder);
                if (Directory.Exists(directory))
                {
                    Directory.Delete(Path.Combine(Path.GetTempPath(), OutputOptions.Instance.BenchmarksTempFolder), true);
                    Directory.CreateDirectory(directory);
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// GRPC+Protobuf small benchmark.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC")]
        [BenchmarkCategory("Real Small")]
        public async Task<(int RequestSize, int ResponseSize)> GrpcPlusProtobufSmall()
        {
            var response = await this.GrpcClient.SmallSimpleAsync(this.GrpcEmpty);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.GrpcEmpty.CalculateSize();
                var responseSize = response.CalculateSize();
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// REST + JSON small benchmark.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST")]
        [BenchmarkCategory("Real Small")]
        public async Task<(int RequestSize, int ResponseSize)> RestPlusJsonSmall()
        {
            await this.RestClient.BenchmarkAsync();
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.RestClient.RequestSize;
                var responseSize = this.RestClient.ResponseSize;
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// GRPC+Protobuf benchmark. Small list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC")]
        [BenchmarkCategory("Small List")]
        public async Task<(int RequestSize, int ResponseSize)> GrpcPlusProtobufSmallList()
        {
            var response = await this.GrpcClient.SimpleListAsync(this.GrpcRequestListSmall);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.GrpcRequestListSmall.CalculateSize();
                var responseSize = response.CalculateSize();
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// REST + JSON benchmark. Small list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST")]
        [BenchmarkCategory("Small List")]
        public async Task<(int RequestSize, int ResponseSize)> RestPlusJsonSmallList()
        {
            await this.RestClient.BenchmarkAllAsync(this.RestRequestListSmall).ConfigureAwait(false);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.RestClient.RequestSize;
                var responseSize = this.RestClient.ResponseSize;
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// GRPC+Protobuf benchmark. Medium list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC")]
        [BenchmarkCategory("Medium List")]
        public async Task<(int RequestSize, int ResponseSize)> GrpcPlusProtobufMediumList()
        {
            var response = await this.GrpcClient.SimpleListAsync(this.GrpcRequestListMedium);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.GrpcRequestListMedium.CalculateSize();
                var responseSize = response.CalculateSize();
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// REST + JSON benchmark. Medium list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST")]
        [BenchmarkCategory("Medium List")]
        public async Task<(int RequestSize, int ResponseSize)> RestPlusJsonMediumList()
        {
            await this.RestClient.BenchmarkAllAsync(this.RestRequestListMedium).ConfigureAwait(false);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.RestClient.RequestSize;
                var responseSize = this.RestClient.ResponseSize;
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// GRPC+Protobuf benchmark. Large list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC")]
        [BenchmarkCategory("Large List")]
        public async Task<(int RequestSize, int ResponseSize)> GrpcPlusProtobufLargeList()
        {
            var response = await this.GrpcClient.SimpleListAsync(this.GrpcRequestListLarge);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.GrpcRequestListLarge.CalculateSize();
                var responseSize = response.CalculateSize();
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// REST + JSON benchmark. Large list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST")]
        [BenchmarkCategory("Large List")]
        public async Task<(int RequestSize, int ResponseSize)> RestPlusJsonLargeList()
        {
            await this.RestClient.BenchmarkAllAsync(this.RestRequestListLarge).ConfigureAwait(false);
            if (this.ShouldCalculateBandwidth)
            {
                var requestSize = this.RestClient.RequestSize;
                var responseSize = this.RestClient.ResponseSize;
                return (requestSize, responseSize);
            }

            return EmptyBandwidthResult;
        }

        /// <summary>
        /// Global setup, happens once at the start of benchmarking.
        /// </summary>
        [GlobalSetup]
        public void GlobalSetup()
        {
            this.BenchmarkParameters = BenchmarkParameters.ReadFromFile();
            this.GrpcSetupGlobal();
            this.RestSetupGlobal();
        }

        /// <summary>
        /// Iteration setup, happens once for each iteration.
        /// </summary>
        [IterationSetup]
        public void IterationSetup()
        {
            this.RequestGeneratorParameters = new RequestGeneratorParameters();
            this.RestSetupIteration();
            this.GrpcSetupIteration();
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(GrpcPlusProtobufSmall))]
        public async void IterationSetupGrpcPlusProtobufSmall()
        {
            await this.IterationSetupTemplate(nameof(this.GrpcPlusProtobufSmall));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(RestPlusJsonSmall))]
        public async void IterationSetupRestPlusJsonSmall()
        {
            await this.IterationSetupTemplate(nameof(this.RestPlusJsonSmall));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(GrpcPlusProtobufSmallList))]
        public async void IterationSetupGrpcPlusProtobufSmallList()
        {
            await this.IterationSetupTemplate(nameof(this.GrpcPlusProtobufSmallList));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(RestPlusJsonSmallList))]
        public async void IterationSetupRestPlusJsonSmallList()
        {
            await this.IterationSetupTemplate(nameof(this.RestPlusJsonSmallList));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(GrpcPlusProtobufMediumList))]
        public async void IterationSetupGrpcPlusProtobufMediumList()
        {
            await this.IterationSetupTemplate(nameof(this.GrpcPlusProtobufMediumList));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(RestPlusJsonMediumList))]
        public async void IterationSetupRestPlusJsonMediumList()
        {
            await this.IterationSetupTemplate(nameof(this.RestPlusJsonMediumList));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(GrpcPlusProtobufLargeList))]
        public async void IterationSetupGrpcPlusProtobufLargeList()
        {
            await this.IterationSetupTemplate(nameof(this.GrpcPlusProtobufLargeList));
        }

        /// <summary>
        /// Iteration setup for the specified benchmark test.
        /// </summary>
        [IterationSetup(Target = nameof(RestPlusJsonLargeList))]
        public async void IterationSetupRestPlusJsonLargeList()
        {
            await this.IterationSetupTemplate(nameof(this.RestPlusJsonLargeList));
        }

        private async Task IterationSetupTemplate(string benchmarkName)
        {
            this.IterationSetup();
            var bandwidthBenchmarker = new BandwidthBenchmarking(this.BenchmarkParameters);
            var benchmarkResult = bandwidthBenchmarker.BandwidthTest(benchmarkName).Result;
            await BandwidthBenchmarking.SaveToFile(benchmarkName, benchmarkResult);
        }

        private void RestSetupIteration()
        {
            var generator = new RestRequestGenerator(this.RequestGeneratorParameters);
            this.RestRequest = generator.Generate();
            this.RestRequestListSmall = new List<Rest.SimpleRequest>();
            this.RestRequestListMedium = new List<Rest.SimpleRequest>();
            this.RestRequestListLarge = new List<Rest.SimpleRequest>();

            for (var requestIndex = 0; requestIndex < ListSizeSmall; requestIndex++)
            {
                this.RestRequestListSmall.Add(this.RestRequest);
            }

            for (var requestIndex = 0; requestIndex < ListSizeMedium; requestIndex++)
            {
                this.RestRequestListMedium.Add(this.RestRequest);
            }

            for (var requestIndex = 0; requestIndex < ListSizeLarge; requestIndex++)
            {
                this.RestRequestListLarge.Add(this.RestRequest);
            }
        }

        private void RestSetupGlobal()
        {
            this.HttpClient = new HttpClient();
            this.RestClient = new Rest.RestClient($"https://localhost:{this.BenchmarkParameters.RestPort}", this.HttpClient)
            {
                ShouldCalculateBandwidth = this.ShouldCalculateBandwidth,
            };
        }

        private void GrpcSetupIteration()
        {
            var generator = new GrpcRequestGenerator(this.RequestGeneratorParameters);
            this.GrpcRequest = generator.Generate();
            this.GrpcRequestListSmall = new Grpc.SimpleRequestList();
            this.GrpcRequestListMedium = new Grpc.SimpleRequestList();
            this.GrpcRequestListLarge = new Grpc.SimpleRequestList();
            this.GrpcEmpty = new Google.Protobuf.WellKnownTypes.Empty();

            for (var requestIndex = 0; requestIndex <= ListSizeSmall; requestIndex++)
            {
                this.GrpcRequestListSmall.Requests.Add(this.GrpcRequest);
            }

            for (var requestIndex = 0; requestIndex <= ListSizeMedium; requestIndex++)
            {
                this.GrpcRequestListMedium.Requests.Add(this.GrpcRequest);
            }

            for (var requestIndex = 0; requestIndex <= ListSizeLarge; requestIndex++)
            {
                this.GrpcRequestListLarge.Requests.Add(this.GrpcRequest);
            }
        }

        private void GrpcSetupGlobal()
        {
            this.GrpcChannel = GrpcChannel.ForAddress($"https://localhost:{this.BenchmarkParameters.GrpcPort}");
            this.GrpcClient = new BenchmarkerClient(this.GrpcChannel);
        }
    }
}
