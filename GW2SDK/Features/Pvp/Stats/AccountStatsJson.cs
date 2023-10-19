using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

[PublicAPI]
public static class AccountStatsJson
{
    public static AccountStats GetAccountStats(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember pvpRank = new("pvp_rank");
        RequiredMember pvpRankPoints = new("pvp_rank_points");
        RequiredMember pvpRankRollovers = new("pvp_rank_rollovers");
        RequiredMember aggregate = new("aggregate");
        RequiredMember professions = new("professions");
        RequiredMember ladders = new("ladders");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(pvpRank.Name))
            {
                pvpRank.Value = member.Value;
            }
            else if (member.NameEquals(pvpRankPoints.Name))
            {
                pvpRankPoints.Value = member.Value;
            }
            else if (member.NameEquals(pvpRankRollovers.Name))
            {
                pvpRankRollovers.Value = member.Value;
            }
            else if (member.NameEquals(aggregate.Name))
            {
                aggregate.Value = member.Value;
            }
            else if (member.NameEquals(professions.Name))
            {
                professions.Value = member.Value;
            }
            else if (member.NameEquals(ladders.Name))
            {
                ladders.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountStats
        {
            PvpRank = pvpRank.Select(value => value.GetInt32()),
            PvpRankPoints = pvpRankPoints.Select(value => value.GetInt32()),
            PvpRankRollovers = pvpRankRollovers.Select(value => value.GetInt32()),
            Aggregate = aggregate.Select(value => value.GetResults(missingMemberBehavior)),
            Professions = professions.Value.EnumerateObject()
                .ToDictionary(
                    pair => (ProfessionName)Enum.Parse(typeof(ProfessionName), pair.Name, ignoreCase: true),
                    pair => pair.Value.GetResults(missingMemberBehavior)
                ),
            Ladders = ladders.Select(value => value.GetLadders(missingMemberBehavior))
        };
    }
}
