using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Transmutation : Consumable
    {
        [NotNull]
        public int[] Skins { get; set; }
    }
}
