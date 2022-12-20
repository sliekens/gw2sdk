using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class DivisionTierJson
{
    public static DivisionTier GetDivisionTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> points = new("points");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(points.Name))
            {
                points.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DivisionTier { Points = points.GetValue() };
    }
}
