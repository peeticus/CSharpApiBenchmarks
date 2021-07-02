// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Grpc.Services
{
    using System.Threading.Tasks;

    using global::Grpc.Core;

    using static ApiBenchmarks.Grpc.Benchmarker;

    /// <summary>
    /// GRPC benchmarking server service.
    /// </summary>
    public class BenchmarkerService : BenchmarkerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkerService"/> class.
        /// </summary>
        /// <param name="responseGenerator">Used for response generation.</param>
        public BenchmarkerService(ResponseGenerator responseGenerator)
        {
            this.Response = responseGenerator.Generate();
        }

        private SimpleResponse Response { get; }

        /// <summary>
        /// Handles the basic GRPC request.
        /// </summary>
        /// <param name="request">The GRPC request.</param>
        /// <param name="context">GRPC context.</param>
        /// <returns>Our dummy GRPC response.</returns>
        public override async Task<SimpleResponse> Simple(SimpleRequest request, ServerCallContext context)
        {
            return await Task.FromResult(this.Response);
        }
    }
}
