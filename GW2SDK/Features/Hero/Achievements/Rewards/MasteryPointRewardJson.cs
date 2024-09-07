﻿using System.Text.Json;
using GuildWars2.Hero.Masteries;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class MasteryPointRewardJson
{
    public static MasteryPointReward GetMasteryPointReward(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember region = "region";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Mastery"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (region.Match(member))
            {
                region = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MasteryPointReward
        {
            Id = id.Map(static value => value.GetInt32()),
            Region = region.Map(static value => value.GetEnum<MasteryRegionName>())
        };
    }
}
