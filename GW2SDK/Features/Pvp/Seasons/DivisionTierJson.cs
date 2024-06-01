using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class DivisionTierJson
{
    public static DivisionTier GetDivisionTier(this JsonElement json)
    {
        RequiredMember points = "points";

        foreach (var member in json.EnumerateObject())
        {
            if (points.Match(member))
            {
                points = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new DivisionTier { Points = points.Map(static value => value.GetInt32()) };
    }
}
