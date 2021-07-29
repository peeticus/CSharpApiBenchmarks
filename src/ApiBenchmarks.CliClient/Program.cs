// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System;
    using System.Threading.Tasks;

    using BenchmarkDotNet.Running;

    using Serilog;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main program entry point.
        /// </summary>
        /// <param name="args">Runtime arguments.</param>
        /// <returns>Task.</returns>
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            await new ApiBenchmarking().ClearLocalResultsFolder();
            CollectBenchmarkParameters();
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            if (!new WindowsRightsChecker().IsElevated())
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===Did you know, if you run this app with elevated permissions (admin window) you can get CPU information?===");
                Console.ForegroundColor = oldColor;
            }

            Console.WriteLine("Benchmark test finished, results are above. Press any key to exit.");
            Console.ReadKey();
        }

        private static void CollectBenchmarkParameters()
        {
            var benchmarkParameters = new BenchmarkParameters();
            Console.Write("Please enter the port # on which the GRPC server is hosted: ");
            benchmarkParameters.GrpcPort = int.Parse(Console.ReadLine());
            Console.Write("Please enter the port # on which the REST server is hosted: ");
            benchmarkParameters.RestPort = int.Parse(Console.ReadLine());
            Console.Write("Should this be a quick job? Less accuracy, but faster. Y/N: ");
            benchmarkParameters.ShortRun = Console.ReadLine().Trim().Equals("y", StringComparison.OrdinalIgnoreCase);
            benchmarkParameters.SaveToFile();

            /*
            var httpClient = new System.Net.Http.HttpClient();
            var restClient = new Rest.RestClient($"https://localhost:{benchmarkParameters.RestPort}", httpClient);
            var generator = new RestRequestGenerator(RequestGeneratorParameters.ReadFromFile());
            var restRequest = generator.Generate();
            var restResult = restClient.BenchmarkAsync(restRequest);
            Console.WriteLine(JsonSerializer.Serialize(restResult));
            */

            /*
            var grpcChannel = GrpcChannel.ForAddress($"https://localhost:{benchmarkParameters.GrpcPort}");
            var grpcClient = new Grpc.Benchmarker.BenchmarkerClient(grpcChannel);
            var generator = new GrpcRequestGenerator(RequestGeneratorParameters.ReadFromFile());
            var result = grpcClient.Simple(generator.Generate());
            Console.WriteLine(JsonSerializer.Serialize(result));
            */
        }
    }
}
