using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Stats;

internal static class MatchStatsJson
{
    public static MatchStats GetMatchStats(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember deaths = "deaths";
        RequiredMember kills = "kills";
        RequiredMember maps = "maps";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (kills.Match(member))
            {
                kills = member;
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

        return new MatchStats
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Deaths = deaths.Map(static (in JsonElement value) => value.GetDistribution()),
            Kills = kills.Map(static (in JsonElement value) => value.GetDistribution()),
            Maps = maps.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetMapSummary()))
        };
    }
}
