using System.Text.Json;

namespace GuildWars2.Hero.Training;

internal static class SkillsByPaletteJson
{
    public static Dictionary<int, int> GetSkillsByPalette(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedArrayLength(entry.GetArrayLength())
                    );
                }
            }

            map.Add(left.GetInt32(), right.GetInt32());
        }

        return map;
    }
}
