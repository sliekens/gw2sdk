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

        /// <remarks>Name can be an empty string but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <remarks>Flags can be an empty array but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public SkinFlag[] Flags { get; set; }

        /// <remarks>Restrictions can be an empty array but not null.</remarks>
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public SkinRestriction[] Restrictions { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Rarity Rarity { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }
    }
}
