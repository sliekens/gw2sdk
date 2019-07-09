using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Bag : Item
    {
        public int Level { get; set; }

        public bool NoSellOrSort { get; set; }

        public int Size { get; set; }
    }
}