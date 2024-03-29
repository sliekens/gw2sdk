﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardSettingJson
{
    public static LeaderboardSetting GetLeaderboardSetting(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember scoring = "scoring";
        RequiredMember tiers = "tiers";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (member.NameEquals("duration") && member.Value.ValueKind == JsonValueKind.Null)
            {
                // Ignore, seems to be always null
            }
            else if (scoring.Match(member))
            {
                scoring = member;
            }
            else if (tiers.Match(member))
            {
                tiers = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardSetting
        {
            Name = name.Map(value => value.GetStringRequired()),
            ScoringId = scoring.Map(value => value.GetStringRequired()),
            Tiers = tiers.Map(
                values => values.GetList(value => value.GetLeaderboardTier(missingMemberBehavior))
            )
        };
    }
}
