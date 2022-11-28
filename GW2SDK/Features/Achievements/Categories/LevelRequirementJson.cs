using System.Text.Json;
using JetBrains.Annotations;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // TODO: use MissingMemberBehavior
        var min = json[0].GetInt32();
        var max = json[1].GetInt32();
        return new LevelRequirement
        {
            Min = min,
            Max = max
        };
    }
}
