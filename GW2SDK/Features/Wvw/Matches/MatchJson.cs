using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class MatchJson
{
    public static Match GetMatch(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Match
        {
            Id = id.Map(value => value.GetStringRequired()),
            StartTime = startTime.Map(value => value.GetDateTimeOffset()),
            EndTime = endTime.Map(value => value.GetDateTimeOffset()),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior)),
            Worlds = worlds.Map(value => value.GetWorlds(missingMemberBehavior)),
            AllWorlds = allWorlds.Map(value => value.GetAllWorlds(missingMemberBehavior)),
            Deaths = deaths.Map(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Map(value => value.GetDistribution(missingMemberBehavior)),
            VictoryPoints =
                victoryPoints.Map(value => value.GetDistribution(missingMemberBehavior)),
            Skirmishes =
                skirmishes.Map(
                    values => values.GetList(value => value.GetSkirmish(missingMemberBehavior))
                ),
            Maps = maps.Map(values => values.GetList(value => value.GetMap(missingMemberBehavior)))
        };
    }
}
