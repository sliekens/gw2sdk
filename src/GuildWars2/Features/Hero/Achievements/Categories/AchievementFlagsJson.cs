using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Categories;

internal static class AchievementFlagsJson
{
    public static AchievementFlags GetAchievementFlags(this in JsonElement json)
    {
        bool specialEvent = false;
        bool pve = false;
        ImmutableList<string>.Builder others = ImmutableList.CreateBuilder<string>();
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
            Other = new ImmutableValueList<string>(others.ToImmutable())
        };
    }
}
