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
        RequiredMember pve = new("pve");
        RequiredMember pvp = new("pvp");
        RequiredMember wvw = new("wvw");
        RequiredMember fractals = new("fractals");
        RequiredMember special = new("special");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(pve.Name))
            {
                pve.Value = member.Value;
            }
            else if (member.NameEquals(pvp.Name))
            {
                pvp.Value = member.Value;
            }
            else if (member.NameEquals(wvw.Name))
            {
                wvw.Value = member.Value;
            }
            else if (member.NameEquals(fractals.Name))
            {
                fractals.Value = member.Value;
            }
            else if (member.NameEquals(special.Name))
            {
                special.Value = member.Value;
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
