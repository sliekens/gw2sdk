using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Skins.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(SkinDiscriminatorOptions))]
    public class Skin
    {
        public int Id { get; set; }

        /// <remarks>Name can be an empty string but not null.</remarks>
        [NotNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Description { get; set; }

        /// <remarks>Flags can be an empty array but not null.</remarks>
        public SkinFlag[] Flags { get; set; }

        /// <remarks>Restrictions can be an empty array but not null.</remarks>
        public SkinRestriction[] Restrictions { get; set; }

        public Rarity Rarity { get; set; }

        [CanBeNull]
        public string Icon { get; set; }
    }
}
