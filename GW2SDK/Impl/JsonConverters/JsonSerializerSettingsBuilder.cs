using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GW2SDK.Impl.JsonConverters
{
    public sealed class JsonSerializerSettingsBuilder
    {
        private readonly NamingStrategy _namingStrategy = new SnakeCaseNamingStrategy();

        private MissingMemberHandling _missingMemberHandling;

        private ITraceWriter _traceWriter;

        public JsonSerializerSettingsBuilder UseMissingMemberHandling(MissingMemberHandling missingMemberHandling)
        {
            _missingMemberHandling = missingMemberHandling;
            return this;
        }

        public JsonSerializerSettingsBuilder UseTraceWriter(ITraceWriter traceWriter)
        {
            _traceWriter = traceWriter;
            return this;
        }

        public JsonSerializerSettings Build() => new JsonSerializerSettings
        {
            MissingMemberHandling = _missingMemberHandling,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = _namingStrategy
            },
            TraceWriter = _traceWriter
        };
    }
}
