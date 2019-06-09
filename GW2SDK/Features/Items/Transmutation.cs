using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    public sealed class Transmutation : Consumable
    {
        [NotNull]
        public int[] Skins { get; set; }
    }
}