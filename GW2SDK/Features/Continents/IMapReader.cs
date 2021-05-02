using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public interface IMapReader : IJsonReader<Map>
    {
        IJsonReader<int> Id { get; }

        IJsonReader<MapSector> Sector { get; }

        IJsonReader<PointOfInterest> PointOfInterest { get; }

        IJsonReader<MapTask> Task { get; }
    }
}