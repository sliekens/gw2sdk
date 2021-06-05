using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public interface ISkillReader : IJsonReader<Skill>
    {
        IJsonReader<int> Id { get; }
    }
}