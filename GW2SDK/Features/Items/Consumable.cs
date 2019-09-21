using System;
using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public class Consumable : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [JsonProperty("duration_ms", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(MillisecondsConverter))]
        public TimeSpan? Duration { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? ApplyCount { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EffectName { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EffectDescription { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EffectIcon { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? GuildUpgradeId { get; set; }
    }
}
