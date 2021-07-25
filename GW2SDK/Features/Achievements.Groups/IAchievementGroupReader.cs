using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public interface IAchievementGroupReader : IJsonReader<AchievementGroup>
    {
        IJsonReader<string> Id { get; }
    }
}
