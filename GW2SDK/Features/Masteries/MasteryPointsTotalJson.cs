using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

internal static class MasteryPointsTotalJson
{
    public static MasteryPointsTotal GetMasteryPointsTotal(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember region = "region";
        RequiredMember spent = "spent";
        RequiredMember earned = "earned";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(region.Name))
            {
                region = member;
            }
            else if (member.NameEquals(spent.Name))
            {
                spent = member;
            }
            else if (member.NameEquals(earned.Name))
            {
                earned = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPointsTotal
        {
            Region = region.Map(value => value.GetStringRequired()),
            Spent = spent.Map(value => value.GetInt32()),
            Earned = earned.Map(value => value.GetInt32())
        };
    }
}
