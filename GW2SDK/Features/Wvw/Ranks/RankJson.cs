using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Ranks;

[PublicAPI]
public static class RankJson
{
    public static Rank GetRank(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember title = "title";
        RequiredMember minRank = "min_rank";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(title.Name))
            {
                title = member;
            }
            else if (member.NameEquals(minRank.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Title = title.Select(value => value.GetStringRequired()),
            MinRank = minRank.Select(value => value.GetInt32())
        };
    }
}
