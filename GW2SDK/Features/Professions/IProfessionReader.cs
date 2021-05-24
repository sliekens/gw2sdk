using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    public interface IProfessionReader : IJsonReader<Profession>
    {
        IJsonReader<ProfessionName> Id { get; }
    }
}