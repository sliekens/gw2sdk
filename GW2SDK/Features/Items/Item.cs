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

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Rarity Rarity { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int VendorValue { get; set; }

        [JsonProperty(Required = Required.Always)]
        public GameType[] GameTypes { get; set; } = new GameType[0];

        [JsonProperty(Required = Required.Always)]
        public ItemFlag[] Flags { get; set; } = new ItemFlag[0];

        [JsonProperty(Required = Required.Always)]
        public ItemRestriction[] Restrictions { get; set; } = new ItemRestriction[0];

        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; } = "";

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Icon { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ItemUpgrade[]? UpgradesFrom { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ItemUpgrade[]? UpgradesInto { get; set; }
    }
}
