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
        RequiredMember pvpRank = "pvp_rank";
        RequiredMember pvpRankPoints = "pvp_rank_points";
        RequiredMember pvpRankRollovers = "pvp_rank_rollovers";
        RequiredMember aggregate = "aggregate";
        RequiredMember professions = "professions";
        RequiredMember ladders = "ladders";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(pvpRank.Name))
            {
                pvpRank = member;
            }
            else if (member.NameEquals(pvpRankPoints.Name))
            {
                pvpRankPoints = member;
            }
            else if (member.NameEquals(pvpRankRollovers.Name))
            {
                pvpRankRollovers = member;
            }
            else if (member.NameEquals(aggregate.Name))
            {
                aggregate = member;
            }
            else if (member.NameEquals(professions.Name))
            {
                professions = member;
            }
            else if (member.NameEquals(ladders.Name))
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
