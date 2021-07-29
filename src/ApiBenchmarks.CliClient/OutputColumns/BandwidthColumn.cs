// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient.OutputColumns
{
    using System.Collections.Generic;

    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Reports;
    using BenchmarkDotNet.Running;

    /// <summary>
    /// An abstract base column class for dealing with bandwidth usage information.
    /// </summary>
    public abstract class BandwidthColumn : IColumn
    {
        /// <inheritdoc />
        public virtual string Id => nameof(BandwidthColumn);

        /// <inheritdoc />
        public virtual string ColumnName => "Bandwidth";

        /// <inheritdoc />
        public virtual string Legend => "Bandwidth";

        /// <inheritdoc />
        public UnitType UnitType => UnitType.Size;

        /// <inheritdoc />
        public bool AlwaysShow => true;

        /// <inheritdoc />
        public ColumnCategory Category => ColumnCategory.Metric;

        /// <inheritdoc />
        public int PriorityInCategory => 0;

        /// <inheritdoc />
        public bool IsNumeric => true;

        /// <inheritdoc />
        public bool IsAvailable(Summary summary) => true;

        /// <inheritdoc />
        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

        /// <inheritdoc />
        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => this.GetValue(summary, benchmarkCase, SummaryStyle.Default);

        /// <inheritdoc />
        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        {
            var benchmarkName = benchmarkCase.Descriptor.WorkloadMethod.Name.ToLower();
            var benchmarkResults = BandwidthBenchmarking.LoadFromFile(benchmarkName).Result;
            double? bandwidth = this.CalculateBandwidthUsage(benchmarkResults);
            return bandwidth != null && bandwidth.HasValue ? bandwidth.Value.ToString() : "error";
        }

        /// <inheritdoc />
        public override string ToString() => this.ColumnName;

        /// <summary>
        /// Calculates bandwidth usage.
        /// </summary>
        /// <param name="benchmarkResults">The benchmark test results.</param>
        /// <returns>Bandwidth usage.</returns>
        protected abstract double CalculateBandwidthUsage(IList<(int RequestSize, int ResponseSize)> benchmarkResults);
    }
}
