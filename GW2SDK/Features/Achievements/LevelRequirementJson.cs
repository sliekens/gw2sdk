using System.Text.Json;

namespace GuildWars2.Achievements;

internal static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
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
