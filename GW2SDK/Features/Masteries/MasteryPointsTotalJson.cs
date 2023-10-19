using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

[PublicAPI]
public static class MasteryPointsTotalJson
{
    public static MasteryPointsTotal GetMasteryPointsTotal(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember region = new("region");
        RequiredMember spent = new("spent");
        RequiredMember earned = new("earned");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(region.Name))
            {
                region.Value = member.Value;
            }
            else if (member.NameEquals(spent.Name))
            {
                spent.Value = member.Value;
            }
            else if (member.NameEquals(earned.Name))
            {
                earned.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPointsTotal
        {
            Region = region.Select(value => value.GetStringRequired()),
            Spent = spent.Select(value => value.GetInt32()),
            Earned = earned.Select(value => value.GetInt32())
        };
    }
}
