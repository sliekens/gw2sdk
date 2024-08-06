using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class AccountWvwSummaryJson
{
    public static AccountWvwSummary GetAccountWvwSummary(this JsonElement json)
    {
        NullableMember teamId = "team_id";
        NullableMember rank = "rank";

        foreach (var member in json.EnumerateObject())
        {
            if (teamId.Match(member))
            {
                teamId = member;
            }
            else if (rank.Match(member))
            {
                rank = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AccountWvwSummary
        {
            TeamId = teamId.Map(static value => value.GetInt32()) switch
            {
                var id when id == 0 => null,
                var id => id
            },
            Rank = rank.Map(static value => value.GetInt32())
        };
    }
}
