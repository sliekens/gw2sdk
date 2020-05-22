using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    public class Skin
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }

        [JsonProperty(Required = Required.Always)]
        public SkinFlag[] Flags { get; set; } = new SkinFlag[0];

        [JsonProperty(Required = Required.Always)]
        public SkinRestriction[] Restrictions { get; set; } = new SkinRestriction[0];

        [JsonProperty(Required = Required.Always)]
        public Rarity Rarity { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Icon { get; set; }
    }
}
