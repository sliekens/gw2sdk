using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public interface IContinentReader : IJsonReader<Continent>
    {
        IJsonReader<int> Id { get; }

        IFloorReader Floor { get; }
    }
}
