using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class MapJson
{
    public static Map GetMap(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<MapKind> type = new("type");
        RequiredMember<Distribution> scores = new("scores");
        RequiredMember<Bonus> bonuses = new("bonuses");
        RequiredMember<Objective> objectives = new("objectives");
        RequiredMember<Distribution> deaths = new("deaths");
        RequiredMember<Distribution> kills = new("kills");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(scores.Name))
            {
                scores.Value = member.Value;
            }
            else if (member.NameEquals(bonuses.Name))
            {
                bonuses.Value = member.Value;
            }
            else if (member.NameEquals(objectives.Name))
            {
                objectives.Value = member.Value;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths.Value = member.Value;
            }
            else if (member.NameEquals(kills.Name))
            {
                kills.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Map
        {
            Id = id.GetValue(),
            Kind = type.GetValue(missingMemberBehavior),
            Scores = scores.Select(value => value.GetDistribution(missingMemberBehavior)),
            Bonuses = bonuses.SelectMany(value => value.GetBonus(missingMemberBehavior)),
            Objectives = objectives.SelectMany(value => value.GetObjective(missingMemberBehavior)),
            Deaths = deaths.Select(value => value.GetDistribution(missingMemberBehavior)),
            Kills = kills.Select(value => value.GetDistribution(missingMemberBehavior))
        };
    }
}
