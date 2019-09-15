using System;
using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ConsumableDiscriminatorOptions))]
    public class Consumable : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [JsonProperty("duration_ms", Required = Required.DisallowNull)]
        [JsonConverter(typeof(MillisecondsConverter))]
        public TimeSpan? Duration { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? ApplyCount { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_name", Required = Required.DisallowNull)]
        public string EffectName { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_description", Required = Required.DisallowNull)]
        public string EffectDescription { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_icon", Required = Required.DisallowNull)]
        public string EffectIcon { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? GuildUpgradeId { get; set; }
    }
}
