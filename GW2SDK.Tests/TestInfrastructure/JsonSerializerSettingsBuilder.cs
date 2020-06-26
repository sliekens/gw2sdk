﻿using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GW2SDK.Tests.TestInfrastructure
{
    internal sealed class JsonSerializerSettingsBuilder
    {
        private readonly NamingStrategy _namingStrategy = new SnakeCaseNamingStrategy();

        private MissingMemberHandling _missingMemberHandling;

        private ITraceWriter _traceWriter;

        internal JsonSerializerSettingsBuilder UseMissingMemberHandling(MissingMemberHandling missingMemberHandling)
        {
            _missingMemberHandling = missingMemberHandling;
            return this;
        }

        internal JsonSerializerSettingsBuilder UseTraceWriter(ITraceWriter traceWriter)
        {
            _traceWriter = traceWriter;
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