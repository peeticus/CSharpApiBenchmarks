// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;

    using global::Grpc.Net.Client;

    using static ApiBenchmarks.Grpc.Benchmarker;

    /// <summary>
    /// Performs benchmark tests.
    /// </summary>
    [EventPipeProfiler(EventPipeProfile.CpuSampling)]
    [Config(typeof(BenchmarkConfig))]
    [HtmlExporter]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class ApiBenchmarking
    {
        private const int ListSizeSmall = 1;
        private const int ListSizeMedium = 5;
        private const int ListSizeLarge = 20;

        /// <summary>
        /// Finalizes an instance of the <see cref="ApiBenchmarking"/> class.
        /// </summary>
        ~ApiBenchmarking()
        {
            this.HttpClient.Dispose();
        }

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
        /// GRPC+Protobuf small benchmark.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC+Proto No Req Small Resp")]
        [BenchmarkCategory("No Req Small Resp")]
        public async Task GrpcPlusProtobufSmall()
        {
            await this.GrpcClient.SmallSimpleAsync(this.GrpcEmpty);
        }

        /// <summary>
        /// REST + JSON small benchmark.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST+JSON No Req Small Resp")]
        [BenchmarkCategory("No Req Small Resp")]
        public async Task RestPlusJsonSmall()
        {
            await this.RestClient.BenchmarkAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// GRPC+Protobuf benchmark. Small list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC+Proto Small List")]
        [BenchmarkCategory("Small List")]
        public async Task GrpcPlusProtobufSmallList()
        {
            await this.GrpcClient.SimpleListAsync(this.GrpcRequestListSmall);
        }

        /// <summary>
        /// REST + JSON benchmark. Small list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST+JSON Small List")]
        [BenchmarkCategory("Small List")]
        public async Task RestPlusJsonSmallList()
        {
            await this.RestClient.BenchmarkAllAsync(this.RestRequestListSmall).ConfigureAwait(false);
        }

        /// <summary>
        /// GRPC+Protobuf benchmark. Medium list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC+Proto Medium List")]
        [BenchmarkCategory("Medium List")]
        public async Task GrpcPlusProtobufMediumList()
        {
            await this.GrpcClient.SimpleListAsync(this.GrpcRequestListMedium);
        }

        /// <summary>
        /// REST + JSON benchmark. Medium list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST+JSON Medium List")]
        [BenchmarkCategory("Medium List")]
        public async Task RestPlusJsonMediumList()
        {
            await this.RestClient.BenchmarkAllAsync(this.RestRequestListMedium).ConfigureAwait(false);
        }

        /// <summary>
        /// GRPC+Protobuf benchmark. Large list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Baseline = true, Description = "GRPC+Proto Large List")]
        [BenchmarkCategory("Large List")]
        public async Task GrpcPlusProtobufLargeList()
        {
            await this.GrpcClient.SimpleListAsync(this.GrpcRequestListLarge);
        }

        /// <summary>
        /// REST + JSON benchmark. Large list size.
        /// </summary>
        /// <returns>Task.</returns>
        [Benchmark(Description = "REST+JSON Large List")]
        [BenchmarkCategory("Large List")]
        public async Task RestPlusJsonLargeList()
        {
            await this.RestClient.BenchmarkAllAsync(this.RestRequestListLarge).ConfigureAwait(false);
        }

        /// <summary>
        /// Global setup, happens once at the start of benchmarking.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            this.RequestGeneratorParameters = RequestGeneratorParameters.ReadFromFile();
            this.BenchmarkParameters = BenchmarkParameters.ReadFromFile();
            this.GrpcSetup();
            this.RestSetup();
        }

        private void RestSetup()
        {
            this.HttpClient = new HttpClient();
            this.RestClient = new Rest.RestClient($"https://localhost:{this.BenchmarkParameters.RestPort}", this.HttpClient);
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

        private void GrpcSetup()
        {
            var generator = new GrpcRequestGenerator(this.RequestGeneratorParameters);
            this.GrpcChannel = GrpcChannel.ForAddress($"https://localhost:{this.BenchmarkParameters.GrpcPort}");
            this.GrpcClient = new BenchmarkerClient(this.GrpcChannel);
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
    }
}
