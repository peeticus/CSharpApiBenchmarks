// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Rest.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiBenchmarks.Dto;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// API controller for benchmark testing.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BenchmarkController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkController"/> class.
        /// </summary>
        public BenchmarkController()
        {
            this.SimpleResponse = new ResponseGenerator().Generate();
        }

        private SimpleResponse SimpleResponse { get; }

        /// <summary>
        /// Accepts a dummy request and returns a dummy response.
        /// </summary>
        /// <returns>Dummy response.</returns>
        /// <param name="request">Dummy request.</param>
        [HttpPost]
        public async Task<IList<SimpleResponse>> Post(IList<SimpleRequest> request)
        {
            var result = new List<SimpleResponse>();
            for (var requestIndex = 0; requestIndex < request.Count; requestIndex++)
            {
                result.Add(this.SimpleResponse);
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Simple get that returns a simple response.
        /// </summary>
        /// <returns>Dummy simple response.</returns>
        [HttpGet]
        public async Task<SimpleResponse> Get()
        {
            return await Task.FromResult(this.SimpleResponse);
        }
    }
}
