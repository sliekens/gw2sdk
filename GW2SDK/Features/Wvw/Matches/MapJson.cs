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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == type.Name)
            {
                type = member;
            }
            else if (member.Name == scores.Name)
            {
                scores = member;
            }
            else if (member.Name == bonuses.Name)
            {
                bonuses = member;
            }
            else if (member.Name == objectives.Name)
            {
                objectives = member;
            }
            else if (member.Name == deaths.Name)
            {
                deaths = member;
            }
            else if (member.Name == kills.Name)
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
