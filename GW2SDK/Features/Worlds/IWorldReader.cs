using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public interface IWorldReader : IJsonReader<World>
    {
        IJsonReader<int> Id { get; }
    }
}
