using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record Transmutation : Consumable
    {
        public int[] Skins { get; init; } = new int[0];
    }
}
