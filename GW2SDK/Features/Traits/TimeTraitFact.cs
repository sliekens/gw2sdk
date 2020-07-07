using System;
using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class TimeTraitFact : TraitFact
    {
        [JsonProperty("duration", Required = Required.Always)]
        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan Duration { get; set; }
    }
}