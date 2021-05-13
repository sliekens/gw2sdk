using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public interface IDailyAchievementReader : IJsonReader<DailyAchievementGroup>
    {
    }
}
