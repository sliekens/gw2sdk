using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class SkillsByPaletteJson
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
            RequiredMember<int> key = new("key");
            RequiredMember<int> value = new("value");

            foreach (var keyOrValue in entry.EnumerateArray())
            {
                if (key.IsUndefined)
                {
                    key.Value = keyOrValue;
                }
                else if (value.IsUndefined)
                {
                    value.Value = keyOrValue;
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedArrayLength(entry.GetArrayLength()));
                }
            }

            map[key.GetValue()] = value.GetValue();
        }

        return map;
    }
}
