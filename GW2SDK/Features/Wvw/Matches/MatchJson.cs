using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class MatchJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(startTime.Name))
            {
                startTime = member;
            }
            else if (member.NameEquals(endTime.Name))
            {
                endTime = member;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores = member;
            }
            else if (member.NameEquals(worlds.Name))
            {
                worlds = member;
            }
            else if (member.NameEquals(allWorlds.Name))
            {
                allWorlds = member;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths = member;
            }
            else if (member.NameEquals(kills.Name))
            {
                kills = member;
            }
            else if (member.NameEquals(victoryPoints.Name))
            {
                victoryPoints = member;
            }
            else if (member.NameEquals(skirmishes.Name))
            {
                skirmishes = member;
            }
            else if (member.NameEquals(maps.Name))
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
            Skirmishes = skirmishes.Map(values => values.GetList(value => value.GetSkirmish(missingMemberBehavior))),
            Maps = maps.Map(values => values.GetList(value => value.GetMap(missingMemberBehavior)))
        };
    }
}
