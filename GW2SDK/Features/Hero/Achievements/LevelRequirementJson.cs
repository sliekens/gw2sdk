using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(
        this JsonElement json
    )
    {
        JsonElement min = default;
        JsonElement max = default;

        foreach (var entry in json.EnumerateArray())
        {
            if (min.ValueKind == JsonValueKind.Undefined)
            {
                min = entry;
            }
            else if (max.ValueKind == JsonValueKind.Undefined)
            {
                max = entry;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(
                    Strings.UnexpectedArrayLength(json.GetArrayLength())
                );
            }
        }

        return new LevelRequirement
        {
            Min = min.GetInt32(),
            Max = max.GetInt32()
        };
    }
}
