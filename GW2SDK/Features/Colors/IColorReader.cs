using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public interface IColorReader : IJsonReader<Color>
    {
        IJsonReader<int> Id { get; }
    }
}
