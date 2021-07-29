// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace Rest
{
    /// <summary>
    /// Our REST + JSON client.
    /// </summary>
    public partial class RestClient
    {
        /// <summary>
        /// Gets or sets the size of the request.
        /// </summary>
        public int RequestSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the response.
        /// </summary>
        public int ResponseSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bandwidth used should be calculated.
        /// </summary>
        public bool ShouldCalculateBandwidth { get; set; }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <param name="response">The HTTP response.</param>
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response)
        {
            if (this.ShouldCalculateBandwidth && response?.Content != null)
            {
                this.ResponseSize = (int)response.Content.ReadAsByteArrayAsync().Result.Length;
                this.ResponseSize += response.Headers.ToString().Length;
            }
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <param name="request">The HTTP request.</param>
        /// <param name="url">The URL we're going to hit.</param>
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            if (this.ShouldCalculateBandwidth && request != null)
            {
                this.RequestSize = request.Headers.ToString().Length;
                if (request.Content != null)
                {
                    this.RequestSize += request.Content.ReadAsByteArrayAsync().Result.Length;
                }
            }
        }
    }
}
