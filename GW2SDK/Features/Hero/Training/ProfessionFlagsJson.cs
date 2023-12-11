using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class ProfessionFlagsJson
{
    public static ProfessionFlags GetProfessionFlags(this JsonElement json)
    {
        var noWeaponSwap = false;
        var noRacialSkills = false;
        List<string>? others = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("NoWeaponSwap"))
            {
                noWeaponSwap = true;
            }
            else if (entry.ValueEquals("NoRacialSkills"))
            {
                noRacialSkills = true;
            }

            else
            {
                others ??= [];
                others.Add(entry.GetStringRequired());
            }
        }

        return new ProfessionFlags
        {
            NoWeaponSwap = noWeaponSwap,
            NoRacialSkills = noRacialSkills,
            Other = others ?? Empty.ListOfString
        };
    }
}
