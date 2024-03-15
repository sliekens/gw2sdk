using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class MapJson
{
    public static Map GetMap(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember scores = "scores";
        RequiredMember bonuses = "bonuses";
        RequiredMember objectives = "objectives";
        RequiredMember deaths = "deaths";
        RequiredMember kills = "kills";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (scores.Match(member))
            {
                scores = member;
            }
            else if (bonuses.Match(member))
            {
                bonuses = member;
            }
            else if (objectives.Match(member))
            {
                objectives = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (kills.Match(member))
            {
                kills = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Map
        {
            Id = id.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Scores = scores.Map(value => value.GetDistribution(missingMemberBehavior)),
            Bonuses =
                bonuses.Map(
                    values => values.GetList(value => value.GetBonus(missingMemberBehavior))
                ),
            Objectives =
                objectives.Map(
                    values => values.GetList(value => value.GetObjective(missingMemberBehavior))
                ),
            Deaths = deaths.Map(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Map(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
