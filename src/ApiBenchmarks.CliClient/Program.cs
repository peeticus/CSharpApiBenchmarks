// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System;
    using System.Text.Json;

    using BenchmarkDotNet.Running;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main program entry point.
        /// </summary>
        /// <param name="args">Runtime arguments.</param>
        public static void Main(string[] args)
        {
            HandleRequestGeneratorParameters();
            CollectBenchmarkParameters();
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.WriteLine("Benchmark test finished, results are above. Press any key to exit.");
            Console.ReadKey();
        }

        private static void HandleRequestGeneratorParameters()
        {
            var requestGeneratorParameters = new RequestGeneratorParameters();
            requestGeneratorParameters.SaveToFile();
            Console.WriteLine("The generation parameters for this run are: ");
            Console.WriteLine(JsonSerializer.Serialize(requestGeneratorParameters));
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
