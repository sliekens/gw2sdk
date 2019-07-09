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
        public int Level { get; set; }

        [JsonProperty("duration_ms")]
        [JsonConverter(typeof(MillisecondsConverter))]
        public TimeSpan? Duration { get; set; }

        public int? ApplyCount { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_name")]
        public string EffectName { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_description")]
        public string EffectDescription { get; set; }

        [CanBeNull]
        [JsonProperty("consumable_icon")]
        public string EffectIcon { get; set; }

        public int? GuildUpgradeId { get; set; }
    }
}
