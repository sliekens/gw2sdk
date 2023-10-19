using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
public static class DailyAchievementGroupJson
{
    public static DailyAchievementGroup GetDailyAchievementGroup(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember pve = "pve";
        RequiredMember pvp = "pvp";
        RequiredMember wvw = "wvw";
        RequiredMember fractals = "fractals";
        RequiredMember special = "special";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(pve.Name))
            {
                pve = member;
            }
            else if (member.NameEquals(pvp.Name))
            {
                pvp = member;
            }
            else if (member.NameEquals(wvw.Name))
            {
                wvw = member;
            }
            else if (member.NameEquals(fractals.Name))
            {
                fractals = member;
            }
            else if (member.NameEquals(special.Name))
            {
                special = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DailyAchievementGroup
        {
            Pve = pve.SelectMany(value => value.GetDailyAchievement(missingMemberBehavior)),
            Pvp = pvp.SelectMany(value => value.GetDailyAchievement(missingMemberBehavior)),
            Wvw = wvw.SelectMany(value => value.GetDailyAchievement(missingMemberBehavior)),
            Fractals =
                fractals.SelectMany(value => value.GetDailyAchievement(missingMemberBehavior)),
            Special = special.SelectMany(
                value => value.GetDailyAchievement(missingMemberBehavior)
            )
        };
    }
}
