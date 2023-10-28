using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Dailies;

internal static class DailyAchievementGroupJson
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
            if (member.Name == pve.Name)
            {
                pve = member;
            }
            else if (member.Name == pvp.Name)
            {
                pvp = member;
            }
            else if (member.Name == wvw.Name)
            {
                wvw = member;
            }
            else if (member.Name == fractals.Name)
            {
                fractals = member;
            }
            else if (member.Name == special.Name)
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
            Pve =
                pve.Map(
                    values =>
                        values.GetList(
                            value => value.GetDailyAchievement(missingMemberBehavior)
                        )
                ),
            Pvp =
                pvp.Map(
                    values =>
                        values.GetList(
                            value => value.GetDailyAchievement(missingMemberBehavior)
                        )
                ),
            Wvw =
                wvw.Map(
                    values =>
                        values.GetList(
                            value => value.GetDailyAchievement(missingMemberBehavior)
                        )
                ),
            Fractals =
                fractals.Map(
                    values =>
                        values.GetList(
                            value => value.GetDailyAchievement(missingMemberBehavior)
                        )
                ),
            Special = special.Map(
                values => values.GetList(
                    value => value.GetDailyAchievement(missingMemberBehavior)
                )
            )
        };
    }
}
