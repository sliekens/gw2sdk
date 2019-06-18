using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public sealed class CraftingMaterial : Item
    {
        public int Level { get; set; }
    }
}