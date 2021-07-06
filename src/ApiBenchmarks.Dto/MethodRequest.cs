// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// A method in the request.
    /// </summary>
    public class MethodRequest
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets method visibility.
        /// </summary>
        public string Visibililty { get; set; }

        /// <summary>
        /// Gets or sets the return type of the method.
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// Gets or sets the method parameters.
        /// </summary>
        public IList<FieldRequest> Parameters { get; set; }
    }
}
