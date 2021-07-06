// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// A dummy request.
    /// </summary>
    public class SimpleRequest
    {
        /// <summary>
        /// Gets or sets the request file.
        /// </summary>
        public FileRequest File { get; set; }

        /// <summary>
        /// Gets or sets a list of request classes.
        /// </summary>
        public IList<ClassRequest> Classes { get; set; }
    }
}
