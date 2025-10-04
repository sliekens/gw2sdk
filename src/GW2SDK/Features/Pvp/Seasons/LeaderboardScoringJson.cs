using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class LeaderboardScoringJson
{
    public static LeaderboardScoring GetLeaderboardScoring(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember type = "type";
        RequiredMember description = "description";
        RequiredMember name = "name";
        RequiredMember ordering = "ordering";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new LeaderboardScoring
        {
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Type = type.Map(static (in value) => value.GetStringRequired()),
            Description = description.Map(static (in value) => value.GetStringRequired()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Ordering = ordering.Map(static (in value) => value.GetStringRequired())
        };
    }
}
