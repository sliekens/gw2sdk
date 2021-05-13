using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public interface IRegionReader : IJsonReader<Region>
    {
        IJsonReader<int> Id { get; }

        IMapReader Map { get; }
    }

}