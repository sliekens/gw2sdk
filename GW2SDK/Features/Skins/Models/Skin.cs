using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    public record Skin
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public string Description { get; init; } = "";

        public SkinFlag[] Flags { get; init; } = new SkinFlag[0];

        public SkinRestriction[] Restrictions { get; init; } = new SkinRestriction[0];

        public Rarity Rarity { get; init; }

        public string? Icon { get; init; }
    }
}
