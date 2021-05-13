using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public interface IAchievementReader : IJsonReader<Achievement>
    {
        IJsonReader<int> Id { get; }

        IJsonReader<DefaultAchievement> Default { get; }

        IJsonReader<ItemSetAchievement> ItemSet { get; }
    }
}
