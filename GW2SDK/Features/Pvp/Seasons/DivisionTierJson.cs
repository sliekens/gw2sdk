using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class DivisionTierJson
{
    public static DivisionTier GetDivisionTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember points = "points";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == points.Name)
            {
                points = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DivisionTier { Points = points.Map(value => value.GetInt32()) };
    }
}
