using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public interface IAchievementGroupReader : IJsonReader<AchievementGroup>
    {
        IJsonReader<string> Id { get; }
    }
}
