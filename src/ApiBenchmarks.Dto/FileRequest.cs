// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a file.
    /// </summary>
    public class FileRequest
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public IList<string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the date/time last modified, UTC.
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Gets or sets the date/time created, UTC.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the full file path.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets the owner of the file.
        /// </summary>
        public string Owner { get; set; }
    }
}
