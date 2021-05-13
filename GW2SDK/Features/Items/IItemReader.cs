using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public interface IItemReader : IJsonReader<Item>
    {
        IJsonReader<int> Id { get; }
    }
}
