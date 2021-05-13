using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record CraftingMaterial : Item
    {
        public ItemUpgrade[]? UpgradesInto { get; init; }
    }
}
