using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public interface ITitleReader : IJsonReader<Title>
    {
        IJsonReader<int> Id { get; }
    }
}
