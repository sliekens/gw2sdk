﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class MatchJson
{
    public static Match GetMatch(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember startTime = "start_time";
        RequiredMember endTime = "end_time";
        RequiredMember scores = "scores";
        RequiredMember worlds = "worlds";
        RequiredMember allWorlds = "all_worlds";
        RequiredMember deaths = "deaths";
        RequiredMember kills = "kills";
        RequiredMember victoryPoints = "victory_points";
        RequiredMember skirmishes = "skirmishes";
        RequiredMember maps = "maps";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (startTime.Match(member))
            {
                startTime = member;
            }
            else if (endTime.Match(member))
            {
                endTime = member;
            }
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (worlds.Match(member))
            {
                worlds = member;
            }
            else if (allWorlds.Match(member))
            {
                allWorlds = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (kills.Match(member))
            {
                kills = member;
            }
            else if (victoryPoints.Match(member))
            {
                victoryPoints = member;
            }
            else if (skirmishes.Match(member))
            {
                skirmishes = member;
            }
            else if (maps.Match(member))
            {
                maps = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Match
        {
            Id = id.Map(static value => value.GetStringRequired()),
            StartTime = startTime.Map(static value => value.GetDateTimeOffset()),
            EndTime = endTime.Map(static value => value.GetDateTimeOffset()),
            Scores = scores.Map(static value => value.GetDistribution()),
            Worlds = worlds.Map(static value => value.GetWorlds()),
            AllWorlds = allWorlds.Map(static value => value.GetAllWorlds()),
            Deaths = deaths.Map(static value => value.GetDistribution()),
            Kills = kills.Map(static value => value.GetDistribution()),
            VictoryPoints = victoryPoints.Map(static value => value.GetDistribution()),
            Skirmishes =
                skirmishes.Map(
                    static values => values.GetList(static value => value.GetSkirmish())
                ),
            Maps = maps.Map(static values => values.GetList(static value => value.GetMap()))
        };
    }
}
