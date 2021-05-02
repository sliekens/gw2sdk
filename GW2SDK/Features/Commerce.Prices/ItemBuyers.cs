using GW2SDK.Annotations;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ItemBuyers
    {
        public int OpenBuyOrders { get; init; }

        public int BestBid { get; init; }
    }
}
