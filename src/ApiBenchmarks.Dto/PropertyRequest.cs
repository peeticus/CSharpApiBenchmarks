// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.Dto
{
    /// <summary>
    /// A property within a class in the request.
    /// </summary>
    public class PropertyRequest
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property visibility.
        /// </summary>
        public string Visibililty { get; set; }

        /// <summary>
        /// Gets or sets the property's data type.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the property default value.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets a reference to the backing field that the property implements.
        /// </summary>
        public FieldRequest BackingField { get; set; }
    }
}
