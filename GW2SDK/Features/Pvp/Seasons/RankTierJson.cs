using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class RankTierJson
{
    public static RankTier GetRankTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> rating = new("rating");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(rating.Name))
            {
                rating.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RankTier { Rating = rating.GetValue() };
    }
}
