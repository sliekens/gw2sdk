using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    public class Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        /// <remarks>Name can be an empty string but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Rarity Rarity { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int VendorValue { get; set; }

        /// <remarks>GameTypes can be an empty array but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public GameType[] GameTypes { get; set; }

        /// <remarks>Flags can be an empty array but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public ItemFlag[] Flags { get; set; }

        /// <remarks>Restrictions can be an empty array but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public ItemRestriction[] Restrictions { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ItemUpgrade[] UpgradesFrom { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ItemUpgrade[] UpgradesInto { get; set; }
    }
}
