using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> min = new("min");
        RequiredMember<int> max = new("max");

        foreach (var entry in json.EnumerateArray())
        {
            if (min.IsMissing)
            {
                min.Value = entry;
            }
            else if (max.IsMissing)
            {
                max.Value = entry;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedArrayLength(json.GetArrayLength()));
            }
        }

        return new LevelRequirement
        {
            Min = min.GetValue(),
            Max = max.GetValue()
        };
    }
}
