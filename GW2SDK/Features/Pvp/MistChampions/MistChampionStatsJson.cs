﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.MistChampions;

internal static class MistChampionStatsJson
{
    public static MistChampionStats GetMistChampionStats(this in JsonElement json)
    {
        RequiredMember offense = "offense";
        RequiredMember defense = "defense";
        RequiredMember speed = "speed";

        foreach (var member in json.EnumerateObject())
        {
            if (offense.Match(member))
            {
                offense = member;
            }
            else if (defense.Match(member))
            {
                defense = member;
            }
            else if (speed.Match(member))
            {
                speed = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MistChampionStats
        {
            Offense = offense.Map(static (in JsonElement value) => value.GetInt32()),
            Defense = defense.Map(static (in JsonElement value) => value.GetInt32()),
            Speed = speed.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
