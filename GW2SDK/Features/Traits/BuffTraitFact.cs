using System;
using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public class BuffTraitFact : TraitFact
    {
        /// <summary>The duration of the effect applied by the trait, or null when the effect is removed by the trait.</summary>
        [JsonProperty("duration", Required = Required.DisallowNull)]
        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan? Duration { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull)]
        public string? Status { get; set; }

        [JsonProperty("description", Required = Required.DisallowNull)]
        public string? Description { get; set; }

        /// <summary>The number of stacks applied by the trait, or null when the effect is removed by the trait.</summary>
        [JsonProperty("apply_count", Required = Required.DisallowNull)]
        public int? ApplyCount { get; set; }
    }
}