using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ItemSellers
    {
        public int OpenSellOrders { get; init; }

        public int BestAsk { get; init; }
    }
}
