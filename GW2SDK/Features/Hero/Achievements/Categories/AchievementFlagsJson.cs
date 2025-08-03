using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal static class AchievementFlagsJson
{
    public static AchievementFlags GetAchievementFlags(this in JsonElement json)
    {
        var specialEvent = false;
        var pve = false;
        ValueList<string> others = [];
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("SpecialEvent"))
            {
                specialEvent = true;
            }
            else if (entry.ValueEquals("PvE"))
            {
                pve = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new AchievementFlags
        {
            SpecialEvent = specialEvent,
            PvE = pve,
            Other = others
        };
    }
}
