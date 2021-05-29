using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats
{
    [PublicAPI]
    public interface IItemStatReader : IJsonReader<ItemStat>
    {
        IJsonReader<int> Id { get; }
    }
}
