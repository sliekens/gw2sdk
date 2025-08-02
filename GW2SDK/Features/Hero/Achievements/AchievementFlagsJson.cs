using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementFlagsJson
{
    public static AchievementFlags GetAchievementFlags(this in JsonElement json)
    {
        var categoryDisplay = false;
        var daily = false;
        var hidden = false;
        var ignoreNearlyComplete = false;
        var moveToTop = false;
        var permanent = false;
        var pvp = false;
        var repairOnLogin = false;
        var repeatable = false;
        var requiresUnlock = false;
        var weekly = false;
        var monthly = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("CategoryDisplay"))
            {
                categoryDisplay = true;
            }
            else if (entry.ValueEquals("Daily"))
            {
                daily = true;
            }
            else if (entry.ValueEquals("Hidden"))
            {
                hidden = true;
            }
            else if (entry.ValueEquals("IgnoreNearlyComplete"))
            {
                ignoreNearlyComplete = true;
            }
            else if (entry.ValueEquals("MoveToTop"))
            {
                moveToTop = true;
            }
            else if (entry.ValueEquals("Permanent"))
            {
                permanent = true;
            }
            else if (entry.ValueEquals("Pvp"))
            {
                pvp = true;
            }
            else if (entry.ValueEquals("RepairOnLogin"))
            {
                repairOnLogin = true;
            }
            else if (entry.ValueEquals("Repeatable"))
            {
                repeatable = true;
            }
            else if (entry.ValueEquals("RequiresUnlock"))
            {
                requiresUnlock = true;
            }
            else if (entry.ValueEquals("Weekly"))
            {
                weekly = true;
            }
            else if (entry.ValueEquals("Monthly"))
            {
                monthly = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new AchievementFlags
        {
            CategoryDisplay = categoryDisplay,
            Daily = daily,
            Hidden = hidden,
            IgnoreNearlyComplete = ignoreNearlyComplete,
            MoveToTop = moveToTop,
            Permanent = permanent,
            Pvp = pvp,
            RepairOnLogin = repairOnLogin,
            Repeatable = repeatable,
            RequiresUnlock = requiresUnlock,
            Weekly = weekly,
            Monthly = monthly,
            Other = others
        };
    }
}
