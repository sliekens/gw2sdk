using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class MatchJson
{
    public static Match GetMatch(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember startTime = new("start_time");
        RequiredMember endTime = new("end_time");
        RequiredMember scores = new("scores");
        RequiredMember worlds = new("worlds");
        RequiredMember allWorlds = new("all_worlds");
        RequiredMember deaths = new("deaths");
        RequiredMember kills = new("kills");
        RequiredMember victoryPoints = new("victory_points");
        RequiredMember skirmishes = new("skirmishes");
        RequiredMember maps = new("maps");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(startTime.Name))
            {
                startTime.Value = member.Value;
            }
            else if (member.NameEquals(endTime.Name))
            {
                endTime.Value = member.Value;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores.Value = member.Value;
            }
            else if (member.NameEquals(worlds.Name))
            {
                worlds.Value = member.Value;
            }
            else if (member.NameEquals(allWorlds.Name))
            {
                allWorlds.Value = member.Value;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths.Value = member.Value;
            }
            else if (member.NameEquals(kills.Name))
            {
                kills.Value = member.Value;
            }
            else if (member.NameEquals(victoryPoints.Name))
            {
                victoryPoints.Value = member.Value;
            }
            else if (member.NameEquals(skirmishes.Name))
            {
                skirmishes.Value = member.Value;
            }
            else if (member.NameEquals(maps.Name))
            {
                maps.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Match
        {
            Id = id.Select(value => value.GetStringRequired()),
            StartTime = startTime.Select(value => value.GetDateTimeOffset()),
            EndTime = endTime.Select(value => value.GetDateTimeOffset()),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior)),
            Worlds = worlds.Select(value => value.GetWorlds(missingMemberBehavior)),
            AllWorlds = allWorlds.Select(value => value.GetAllWorlds(missingMemberBehavior)),
            Deaths = deaths.Select(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Select(value => value.GetDistribution(missingMemberBehavior)),
            VictoryPoints =
                victoryPoints.Select(value => value.GetDistribution(missingMemberBehavior)),
            Skirmishes = skirmishes.SelectMany(value => value.GetSkirmish(missingMemberBehavior)),
            Maps = maps.SelectMany(value => value.GetMap(missingMemberBehavior))
        };
    }
}
