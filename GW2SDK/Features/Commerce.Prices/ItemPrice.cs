using GW2SDK.Annotations;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record ItemPrice
    {
        public int Id { get; init; }

        public bool Whitelisted { get; init; }

        public ItemBuyers Buyers { get; init; } = new();

        public ItemSellers Sellers { get; init; } = new();
    }
}
