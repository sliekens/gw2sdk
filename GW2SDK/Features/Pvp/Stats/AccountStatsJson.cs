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
        RequiredMember<int> pvpRank = new("pvp_rank");
        RequiredMember<int> pvpRankPoints = new("pvp_rank_points");
        RequiredMember<int> pvpRankRollovers = new("pvp_rank_rollovers");
        RequiredMember<Results> aggregate = new("aggregate");
        RequiredMember<Dictionary<ProfessionName, Results>> professions = new("professions");
        RequiredMember<Ladders> ladders = new("ladders");

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
            PvpRank = pvpRank.GetValue(),
            PvpRankPoints = pvpRankPoints.GetValue(),
            PvpRankRollovers = pvpRankRollovers.GetValue(),
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
