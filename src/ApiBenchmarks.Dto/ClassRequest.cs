// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// A class within a request.
    /// </summary>
    public class ClassRequest
    {
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the class visibility.
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// Gets or sets a list of classes/interfaces that this class request implements.
        /// </summary>
        public IList<string> Implements { get; set; }

        /// <summary>
        /// Gets or sets a list of fields in the class.
        /// </summary>
        public IList<FieldRequest> Fields { get; set; }

        /// <summary>
        /// Gets or sets a list of properties in the class.
        /// </summary>
        public IList<PropertyRequest> Properties { get; set; }

        /// <summary>
        /// Gets or sets a list of methods in the class.
        /// </summary>
        public IList<MethodRequest> Methods { get; set; }
    }
}
