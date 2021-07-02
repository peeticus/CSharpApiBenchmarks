// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text.Json;

    /// <summary>
    /// Parameters used in the generation of benchmarks.
    /// </summary>
    /// <remarks>This thing can save itself to a file and load itself from a file
    /// because the benchmark library is unable to accept parameters. So, we
    /// communicate parameters to it by loading and saving files.</remarks>
    public class RequestGeneratorParameters
    {
        /// <summary>
        /// Gets the number of "class" objects to generate in the request.
        /// </summary>
        public int ClassCount { get; } = RandomNumberGenerator.GetInt32(1, 10);

        /// <summary>
        /// Gets the number of "field" objects to generate in each class in the request.
        /// </summary>
        public int FieldCount { get; } = RandomNumberGenerator.GetInt32(1, 10);

        /// <summary>
        /// Gets the number of "method" objects to generate in each class in the request.
        /// </summary>
        public int MethodCount { get; } = RandomNumberGenerator.GetInt32(1, 10);

        /// <summary>
        /// Gets the number of "parameter" objects to generate in each method in the request.
        /// </summary>
        public int ParameterCount { get; } = RandomNumberGenerator.GetInt32(1, 10);

        /// <summary>
        /// Gets the number of "property" objects to generate in each class in the request.
        /// </summary>
        public int PropertyCount { get; } = RandomNumberGenerator.GetInt32(1, 10);

        /// <summary>
        /// Gets the number of implemented interfaces+classes to generate in each class in the request.
        /// </summary>
        public int ImplementsCount { get; } = RandomNumberGenerator.GetInt32(1, 10);

        /// <summary>
        /// Gets a value indicating whether each "property" in the request will have a backing field.
        /// </summary>
        public bool HasBackingField { get; } = Convert.ToBoolean(RandomNumberGenerator.GetInt32(0, 1));

        /// <summary>
        /// Reads an object of this type from a file in the temp directory.
        /// </summary>
        /// <returns>Populated instance from the file.</returns>
        public static RequestGeneratorParameters ReadFromFile()
        {
            var configContents = File.ReadAllText(Path.Combine(Path.GetTempPath(), $"{nameof(RequestGeneratorParameters)}.txt"));
            return JsonSerializer.Deserialize<RequestGeneratorParameters>(configContents);
        }

        /// <summary>
        /// Saves this object to a file in the temp directory.
        /// </summary>
        public void SaveToFile()
        {
            File.WriteAllText(Path.Combine(Path.GetTempPath(), $"{nameof(RequestGeneratorParameters)}.txt"), JsonSerializer.Serialize(this));
        }
    }
}
