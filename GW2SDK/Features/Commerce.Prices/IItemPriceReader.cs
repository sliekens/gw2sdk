using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI] 
    public interface IItemPriceReader : IJsonReader<ItemPrice>
    {
        IJsonReader<int> Id { get; }
    }
}
