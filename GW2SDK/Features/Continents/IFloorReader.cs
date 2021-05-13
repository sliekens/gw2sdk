using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public interface IFloorReader : IJsonReader<Floor>
    {
        IJsonReader<int> Id { get; }

        IRegionReader Region { get; }
    }
}