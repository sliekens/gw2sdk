using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardScoringJson
{
    public static LeaderboardScoring GetLeaderboardScoring(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember description = "description";
        RequiredMember name = "name";
        RequiredMember ordering = "ordering";

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
            else if (description.Match(member))
            {
                description = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (ordering.Match(member))
            {
                ordering = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardScoring
        {
            Id = id.Map(value => value.GetStringRequired()),
            Type = type.Map(value => value.GetStringRequired()),
            Description = description.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Ordering = ordering.Map(value => value.GetStringRequired())
        };
    }
}
