using System.Text.Json;

using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class MapJson
{
    public static Map GetMap(this in JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Map
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Kind = type.Map(static (in JsonElement value) => value.GetEnum<MapKind>()),
            Scores = scores.Map(static (in JsonElement value) => value.GetDistribution()),
            Bonuses =
                bonuses.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetBonus())),
            Objectives =
                objectives.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetOwnedObjective())
                ),
            Deaths = deaths.Map(static (in JsonElement value) => value.GetDistribution()),
            Kills = kills.Map(static (in JsonElement value) => value.GetDistribution())
        };
    }
}
