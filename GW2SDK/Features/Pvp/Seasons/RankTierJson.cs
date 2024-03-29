﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class RankTierJson
{
    public static RankTier GetRankTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember rating = "rating";

        foreach (var member in json.EnumerateObject())
        {
            if (rating.Match(member))
            {
                rating = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RankTier { Rating = rating.Map(value => value.GetInt32()) };
    }
}
