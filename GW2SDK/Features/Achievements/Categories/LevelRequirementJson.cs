using System.Text.Json;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public static class LevelRequirementJson
{
    public static LevelRequirement GetLevelRequirement(this JsonElement json)
    {
        if (json.GetArrayLength() < 2)
        {
            throw new InvalidOperationException($"Missing level requirement(s).");
        }
        else if (json.GetArrayLength() > 2)
        {
            throw new InvalidOperationException($"Unexpected level requirement(s).");
        }

        var min = json[0].GetInt32();
        var max = json[1].GetInt32();
        return new LevelRequirement
        {
            Min = min,
            Max = max
        };
    }
}
