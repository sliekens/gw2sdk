using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

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
            if (member.NameEquals(totals.Name))
            {
                totals = member;
            }
            else if (member.NameEquals(unlocked.Name))
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
