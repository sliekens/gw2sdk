using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public interface IDailyAchievementReader : IJsonReader<DailyAchievementGroup>
    {
    }
}
