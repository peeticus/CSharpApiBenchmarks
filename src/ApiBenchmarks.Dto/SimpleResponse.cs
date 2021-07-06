// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// A response.
    /// </summary>
    public class SimpleResponse
    {
        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        public string ProgramName { get; set; }

        /// <summary>
        /// Gets or sets a list of standard output lines.
        /// </summary>
        public IList<string> StandardOutput { get; set; }

        /// <summary>
        /// Gets or sets a list of error output lines.
        /// </summary>
        public IList<string> ErrorOutput { get; set; }

        /// <summary>
        /// Gets or sets the program exit code.
        /// </summary>
        public int ExitCode { get; set; }
    }
}
