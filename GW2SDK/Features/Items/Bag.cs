using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public sealed class Bag : Item
    {
        public int Level { get; set; }

        public bool NoSellOrSort { get; set; }

        public int Size { get; set; }
    }
}