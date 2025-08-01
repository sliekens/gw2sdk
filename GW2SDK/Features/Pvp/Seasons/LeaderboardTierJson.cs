﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardTierJson
{
    public static LeaderboardTier GetLeaderboardTier(this in JsonElement json)
    {
        OptionalMember color = "color";
        NullableMember type = "type";
        OptionalMember name = "name";
        RequiredMember range = "range";

        foreach (var member in json.EnumerateObject())
        {
            if (color.Match(member))
            {
                color = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (range.Match(member))
            {
                range = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new LeaderboardTier
        {
            Color = color.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Kind = type.Map(static (in JsonElement value) => value.GetEnum<LeaderboardTierKind>()),
            Name = name.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Range = range.Map(static (in JsonElement value) => value.GetLeaderboardTierRange())
        };
    }
}
