using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Categories;

internal static class AchievementFlagsJson
{
    public static AchievementFlags GetAchievementFlags(this JsonElement json)
    {
        var specialEvent = false;
        var pve = false;
        List<string>? others = null;
        foreach (var entry in json.EnumerateArray())
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
                others ??= new List<string>();
                others.Add(entry.GetStringRequired());
            }
        }

        return new AchievementFlags
        {
            SpecialEvent = specialEvent,
            PvE = pve,
            Other = others ?? Empty.ListOfString
        };
    }
}
