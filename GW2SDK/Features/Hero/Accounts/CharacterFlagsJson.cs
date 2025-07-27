using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class CharacterFlagsJson
{
    public static CharacterFlags GetCharacterFlags(this in JsonElement json)
    {
        var beta = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Beta"))
            {
                beta = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new CharacterFlags
        {
            Beta = beta,
            Other = others
        };
    }
}
