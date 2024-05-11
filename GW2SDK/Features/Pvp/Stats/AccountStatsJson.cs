using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

internal static class AccountStatsJson
{
    public static AccountStats GetAccountStats(this JsonElement json)
    {
        RequiredMember pvpRank = "pvp_rank";
        RequiredMember pvpRankPoints = "pvp_rank_points";
        RequiredMember pvpRankRollovers = "pvp_rank_rollovers";
        RequiredMember aggregate = "aggregate";
        RequiredMember professions = "professions";
        RequiredMember ladders = "ladders";

        foreach (var member in json.EnumerateObject())
        {
            if (pvpRank.Match(member))
            {
                pvpRank = member;
            }
            else if (pvpRankPoints.Match(member))
            {
                pvpRankPoints = member;
            }
            else if (pvpRankRollovers.Match(member))
            {
                pvpRankRollovers = member;
            }
            else if (aggregate.Match(member))
            {
                aggregate = member;
            }
            else if (professions.Match(member))
            {
                professions = member;
            }
            else if (ladders.Match(member))
            {
                ladders = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountStats
        {
            PvpRank = pvpRank.Map(static value => value.GetInt32()),
            PvpRankPoints = pvpRankPoints.Map(static value => value.GetInt32()),
            PvpRankRollovers = pvpRankRollovers.Map(static value => value.GetInt32()),
            Aggregate = aggregate.Map(static value => value.GetResults()),
            Professions =
                professions.Map(
                    static value =>
                        value.EnumerateObject()
                            .ToDictionary(
                                pair =>
                                    (ProfessionName)Enum.Parse(
                                        typeof(ProfessionName),
                                        pair.Name,
                                        true
                                    ),
                                pair => pair.Value.GetResults()
                            )
                ),
            Ladders = ladders.Map(static value => value.GetLadders())
        };
    }
}
