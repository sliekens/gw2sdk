using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryPointsProgressJson
{
    public static MasteryPointsProgress GetMasteryPointsProgress(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember totals = "totals";
        RequiredMember unlocked = "unlocked";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == totals.Name)
            {
                totals = member;
            }
            else if (member.Name == unlocked.Name)
            {
                unlocked = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPointsProgress
        {
            Totals = totals.Map(
                values =>
                    values.GetList(entry => entry.GetMasteryPointsTotal(missingMemberBehavior))
            ),
            Unlocked = unlocked.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
