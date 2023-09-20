using System.Text.Json;

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
        // In C# there are no shortcuts but we can convert it to an IEnumerable<KeyValuePair>
        //  then convert that to Dictionary
        // TODO: use MissingMemberBehavior
        Dictionary<int, int> map = new(json.GetArrayLength());
        foreach (var member in json.EnumerateArray())
        {
            // Short-circuit invalid data
            if (member.GetArrayLength() != 2)
            {
                break;
            }

            var key = member[0].GetInt32();
            var value = member[1].GetInt32();

            map[key] = value;
        }

        return map;
    }
}
