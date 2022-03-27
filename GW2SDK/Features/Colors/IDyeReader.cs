using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public interface IDyeReader : IJsonReader<Dye>
    {
        IJsonReader<int> Id { get; }
    }
}
