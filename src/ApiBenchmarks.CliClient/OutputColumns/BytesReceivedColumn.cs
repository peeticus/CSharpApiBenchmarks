// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.OutputColumns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Columns;

    /// <summary>
    /// A column representing bandwidth usage, bytes received.
    /// </summary>
    public class BytesReceivedColumn : BandwidthColumn
    {
        /// <inheritdoc cref="IColumn" />
        public override string Id => nameof(BytesReceivedColumn);

        /// <inheritdoc cref="IColumn" />
        public override string ColumnName => "B Recd";

        /// <inheritdoc cref="IColumn" />
        public override string Legend => "Network interface bytes received.";

        /// <inheritdoc />
        protected override double CalculateBandwidthUsage(IList<(int RequestSize, int ResponseSize)> benchmarkResults)
        {
            return Math.Round(benchmarkResults.Average(br => br.ResponseSize), 0);
        }
    }
}
