﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Ranks;

internal static class RankJson
{
    public static Rank GetRank(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember minRank = "min_rank";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (title.Match(member))
            {
                title = member;
            }
            else if (minRank.Match(member))
            {
                minRank = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Rank
        {
            Id = id.Map(static value => value.GetInt32()),
            Title = title.Map(static value => value.GetStringRequired()),
            MinRank = minRank.Map(static value => value.GetInt32())
        };
    }
}
