using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Ranks;

internal static class RankJson
{
    public static Rank GetRank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember minRank = "min_rank";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (title.Match(member))
            {
                title = member;
            }
            else if (minRank.Match(member))
            {
                minRank = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Rank
        {
            Id = id.Map(value => value.GetInt32()),
            Title = title.Map(value => value.GetStringRequired()),
            MinRank = minRank.Map(value => value.GetInt32())
        };
    }
}
