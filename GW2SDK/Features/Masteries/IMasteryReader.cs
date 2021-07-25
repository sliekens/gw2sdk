using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Masteries
{
    [PublicAPI]
    public interface IMasteryReader : IJsonReader<Mastery>
    {
        IJsonReader<int> Id { get; }
    }
}
