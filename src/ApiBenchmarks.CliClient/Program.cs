// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System;
    using System.IO;
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
            var requestGeneratorParameters = new RequestGeneratorParameters();
            requestGeneratorParameters.SaveToFile();
            Console.WriteLine("The generation parameters for this run are: ");
            Console.WriteLine(JsonSerializer.Serialize(requestGeneratorParameters));
            Console.Write("Please enter the port # on which the GRPC server is hosted: ");
            var grpcPort = int.Parse(Console.ReadLine());
            File.WriteAllText(ApiBenchmarking.GrpcParametersFilePath, grpcPort.ToString());
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadKey();
        }
    }
}
