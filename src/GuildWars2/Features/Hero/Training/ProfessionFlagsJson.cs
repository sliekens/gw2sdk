using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class ProfessionFlagsJson
{
    public static ProfessionFlags GetProfessionFlags(this in JsonElement json)
    {
        bool noWeaponSwap = false;
        bool noRacialSkills = false;
        ValueList<string> others = [];
        foreach (JsonElement entry in json.EnumerateArray())
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
                others.Add(entry.GetStringRequired());
            }
        }

        return new ProfessionFlags
        {
            NoWeaponSwap = noWeaponSwap,
            NoRacialSkills = noRacialSkills,
            Other = others
        };
    }
}
