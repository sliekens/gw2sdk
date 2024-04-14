using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryPointsProgressJson
{
    public static MasteryPointsProgress GetMasteryPointsProgress(this JsonElement json)
    {
        RequiredMember totals = "totals";
        RequiredMember unlocked = "unlocked";

        foreach (var member in json.EnumerateObject())
        {
            if (totals.Match(member))
            {
                totals = member;
            }
            else if (unlocked.Match(member))
            {
                unlocked = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPointsProgress
        {
            Totals =
                totals.Map(
                    static values =>
                        values.GetList(static value => value.GetMasteryPointsTotal())
                ),
            Unlocked = unlocked.Map(
                static values => values.GetList(static value => value.GetInt32())
            )
        };
    }
}
