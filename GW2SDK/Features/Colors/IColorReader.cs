using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public interface IColorReader : IJsonReader<Color>
    {
        IJsonReader<int> Id { get; }
    }
}
