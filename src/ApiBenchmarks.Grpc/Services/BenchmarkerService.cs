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

        /// <summary>
        /// Handles a basic GRPC request, but accepting and returning objects with a larger list structure
        /// to better test situations with higher bandwidth usage.
        /// </summary>
        /// <param name="request">The GRPC request.</param>
        /// <param name="context">GRPC context.</param>
        /// <returns>Our dummy GRPC response.</returns>
        public override async Task<SimpleResponseList> SimpleList(SimpleRequestList request, ServerCallContext context)
        {
            var response = new SimpleResponseList();
            for (var requestIndex = 0; requestIndex < request.Requests.Count; requestIndex++)
            {
                response.Responses.Add(this.Response);
            }

            return await Task.FromResult(response);
        }

        /// <summary>
        /// No request, smallish response.
        /// </summary>
        /// <param name="empty">No data.</param>
        /// <param name="context">GRPC context.</param>
        /// <returns>Our dummy GRPC response.</returns>
        public override async Task<SimpleResponse> SmallSimple(Google.Protobuf.WellKnownTypes.Empty empty, ServerCallContext context)
        {
            return await Task.FromResult(this.Response);
        }
    }
}
