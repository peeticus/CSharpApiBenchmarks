// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using AutoFixture;

    /// <summary>
    /// Generates a dummy REST request object.
    /// </summary>
    public class RestRequestGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequestGenerator"/> class.
        /// </summary>
        /// <param name="parameters">Parameters used in construction of the objects.</param>
        public RestRequestGenerator(RequestGeneratorParameters parameters)
        {
            this.Parameters = parameters;
        }

        private RequestGeneratorParameters Parameters { get; }

        /// <summary>
        /// Generates a dummy REST request object.
        /// </summary>
        /// <returns>The dummy request object.</returns>
        public Rest.SimpleRequest Generate()
        {
            var fixture = new Fixture();
            var request = fixture.Create<Rest.SimpleRequest>();

            for (var requestClassesIndex = 1; requestClassesIndex <= this.Parameters.ClassCount; requestClassesIndex++)
            {
                var classRequest = fixture.Create<Rest.ClassRequest>();

                for (var fieldIndex = 1; fieldIndex <= this.Parameters.FieldCount; fieldIndex++)
                {
                    classRequest.Fields.Add(fixture.Create<Rest.FieldRequest>());
                }

                for (var methodIndex = 1; methodIndex <= this.Parameters.MethodCount; methodIndex++)
                {
                    var methodRequest = fixture.Create<Rest.MethodRequest>();
                    for (var parameterIndex = 1; parameterIndex <= this.Parameters.ParameterCount; parameterIndex++)
                    {
                        methodRequest.Parameters.Add(fixture.Create<Rest.FieldRequest>());
                    }

                    classRequest.Methods.Add(methodRequest);
                }

                for (var propertyIndex = 1; propertyIndex <= this.Parameters.PropertyCount; propertyIndex++)
                {
                    var propertyRequest = fixture.Create<Rest.PropertyRequest>();
                    propertyRequest.BackingField = this.Parameters.HasBackingField ? new Rest.FieldRequest() : null;
                    classRequest.Properties.Add(propertyRequest);
                }

                for (var implementsIndex = 1; implementsIndex <= this.Parameters.ImplementsCount; implementsIndex++)
                {
                    classRequest.Implements.Add(fixture.Create<string>());
                }

                request.Classes.Add(classRequest);
            }

            request.File = fixture.Create<Rest.FileRequest>();

            return request;
        }
    }
}
