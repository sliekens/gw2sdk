namespace GW2SDK.Features.Items
{
    public sealed class Bag : Item
    {
        public int Level { get; set; }

        public bool NoSellOrSort { get; set; }

        public int Size { get; set; }
    }
}