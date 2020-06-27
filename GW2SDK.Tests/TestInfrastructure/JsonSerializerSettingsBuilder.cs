using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit.Abstractions;

namespace GW2SDK.Tests.TestInfrastructure
{
    internal sealed class JsonSerializerSettingsBuilder
    {
        private readonly NamingStrategy _namingStrategy = new SnakeCaseNamingStrategy();

        private MissingMemberHandling _missingMemberHandling;

        private ITraceWriter _traceWriter;

        internal JsonSerializerSettingsBuilder ThrowErrorOnMissingMember()
        {
            _missingMemberHandling = MissingMemberHandling.Error;
            return this;
        }

        internal JsonSerializerSettingsBuilder UseTraceWriter(ITestOutputHelper output)
        {
            _traceWriter = new XunitTraceWriter(output);
            return this;
        }

        internal JsonSerializerSettings Build() =>
            new JsonSerializerSettings
            {
                MissingMemberHandling = _missingMemberHandling,
                ContractResolver = new DefaultContractResolver { NamingStrategy = _namingStrategy },
                TraceWriter = _traceWriter,
                Converters = Json.GetJsonConverters()
            };
    }
}
