using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public interface IAchievementCategoryReader : IJsonReader<AchievementCategory>
    {
        IJsonReader<int> Id { get; }
    }
}
