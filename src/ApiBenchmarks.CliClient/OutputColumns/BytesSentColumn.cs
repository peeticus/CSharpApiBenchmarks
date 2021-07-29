// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.OutputColumns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Columns;

    /// <summary>
    /// A column representing bandwidth usage, bytes sent.
    /// </summary>
    public class BytesSentColumn : BandwidthColumn
    {
        /// <inheritdoc cref="IColumn" />
        public override string Id => nameof(BytesSentColumn);

        /// <inheritdoc cref="IColumn" />
        public override string ColumnName => "B Sent";

        /// <inheritdoc cref="IColumn" />
        public override string Legend => "Network interface bytes sent.";

        /// <inheritdoc />
        protected override double CalculateBandwidthUsage(IList<(int RequestSize, int ResponseSize)> benchmarkResults)
        {
            return Math.Round(benchmarkResults.Average(br => br.RequestSize), 0);
        }
    }
}