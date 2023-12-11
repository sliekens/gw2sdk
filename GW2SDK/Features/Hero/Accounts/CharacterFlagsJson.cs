using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class CharacterFlagsJson
{
    public static CharacterFlags GetCharacterFlags(this JsonElement json)
    {
        var beta = false;
        List<string>? others = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Beta"))
            {
                beta = true;
            }
            else
            {
                others ??= [];
                others.Add(entry.GetStringRequired());
            }
        }

        return new CharacterFlags
        {
            Beta = beta,
            Other = others ?? Empty.ListOfString
        };
    }
}
