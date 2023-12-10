using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Stats;

internal static class AccountStatsJson
{
    public static AccountStats GetAccountStats(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember pvpRank = "pvp_rank";
        RequiredMember pvpRankPoints = "pvp_rank_points";
        RequiredMember pvpRankRollovers = "pvp_rank_rollovers";
        RequiredMember aggregate = "aggregate";
        RequiredMember professions = "professions";
        RequiredMember ladders = "ladders";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == pvpRank.Name)
            {
                pvpRank = member;
            }
            else if (member.Name == pvpRankPoints.Name)
            {
                pvpRankPoints = member;
            }
            else if (member.Name == pvpRankRollovers.Name)
            {
                pvpRankRollovers = member;
            }
            else if (member.Name == aggregate.Name)
            {
                aggregate = member;
            }
            else if (member.Name == professions.Name)
            {
                professions = member;
            }
            else if (member.Name == ladders.Name)
            {
                ladders = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AccountStats
        {
            PvpRank = pvpRank.Map(value => value.GetInt32()),
            PvpRankPoints = pvpRankPoints.Map(value => value.GetInt32()),
            PvpRankRollovers = pvpRankRollovers.Map(value => value.GetInt32()),
            Aggregate = aggregate.Map(value => value.GetResults(missingMemberBehavior)),
            Professions = professions.Map(
                value => value.EnumerateObject()
                    .ToDictionary(
                        pair => (ProfessionName)Enum.Parse(typeof(ProfessionName), pair.Name, true),
                        pair => pair.Value.GetResults(missingMemberBehavior)
                    )
            ),
            Ladders = ladders.Map(value => value.GetLadders(missingMemberBehavior))
        };
    }
}
