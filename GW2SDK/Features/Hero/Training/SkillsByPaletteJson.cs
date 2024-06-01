using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class SkillsByPaletteJson
{
    public static Dictionary<int, int> GetSkillsByPalette(this JsonElement json)
    {
        // The json is an iterable of key-value pairs
        // e.g.
        // [
        //   [ 1, 12343 ],
        //   [ 2, 12417 ],
        //   [ 3, 12371 ]
        // ]
        //
        // In JavaScript you could just do new Map([[1,12343],[2,12417]])
        // In C# there are no shortcuts
        Dictionary<int, int> map = new(json.GetArrayLength());
        foreach (var entry in json.EnumerateArray())
        {
            JsonElement left = default;
            JsonElement right = default;

            foreach (var keyOrValue in entry.EnumerateArray())
            {
                if (left.ValueKind == JsonValueKind.Undefined)
                {
                    left = keyOrValue;
                }
                else if (right.ValueKind == JsonValueKind.Undefined)
                {
                    right = keyOrValue;
                }
                else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedArrayLength(entry.GetArrayLength());
                }
            }

            map.Add(left.GetInt32(), right.GetInt32());
        }

        return map;
    }
}
