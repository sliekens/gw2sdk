using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    public interface ISkinReader : IJsonReader<Skin>
    {
        IJsonReader<int> Id { get; }
    }
}