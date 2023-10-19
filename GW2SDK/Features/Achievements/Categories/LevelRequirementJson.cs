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
        RequiredMember min = new("[0]");
        RequiredMember max = new("[1]");

        foreach (var entry in json.EnumerateArray())
        {
            if (min.IsUndefined)
            {
                min.Value = entry;
            }
            else if (max.IsUndefined)
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
            Min = min.Select(value => value.GetInt32()),
            Max = max.Select(value => value.GetInt32())
        };
    }
}
