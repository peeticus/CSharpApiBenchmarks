// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    using System.Security.Cryptography;

    using AutoFixture;

    /// <summary>
    /// Generates a random response object.
    /// </summary>
    public class ResponseGenerator
    {
        /// <summary>
        /// Generates a random response object.
        /// </summary>
        /// <returns>A dummy response object.</returns>
        public SimpleResponse Generate()
        {
            var fixture = new Fixture();
            var response = fixture.Create<SimpleResponse>();

            var errorOutputCount = RandomNumberGenerator.GetInt32(0, 10);
            for (var errorOutputIndex = 1; errorOutputIndex <= errorOutputCount; errorOutputIndex++)
            {
                response.ErrorOutput.Add(fixture.Create<string>());
            }

            var standardOutputCount = RandomNumberGenerator.GetInt32(0, 10);
            for (var standardOutputIndex = 1; standardOutputIndex <= standardOutputCount; standardOutputIndex++)
            {
                response.StandardOutput.Add(fixture.Create<string>());
            }

            return response;
        }
    }
}
