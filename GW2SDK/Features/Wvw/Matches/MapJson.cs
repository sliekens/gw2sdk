using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class MapJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores = member;
            }
            else if (member.NameEquals(bonuses.Name))
            {
                bonuses = member;
            }
            else if (member.NameEquals(objectives.Name))
            {
                objectives = member;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths = member;
            }
            else if (member.NameEquals(kills.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Kind = type.Select(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior)),
            Bonuses = bonuses.SelectMany(value => value.GetBonus(missingMemberBehavior)),
            Objectives = objectives.SelectMany(value => value.GetObjective(missingMemberBehavior)),
            Deaths = deaths.Select(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Select(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
