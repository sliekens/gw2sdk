using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardTierJson
{
    public static LeaderboardTier GetLeaderboardTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        OptionalMember color = new("color");
        OptionalMember type = new("type");
        OptionalMember name = new("name");
        RequiredMember range = new("range");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(color.Name))
            {
                color = member;
            }
            else if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(range.Name))
            {
                range = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardTier
        {
            Color = color.Select(value => value.GetString()) ?? "",
            Kind = type.Select(value => value.GetEnum<LeaderboardTierKind>(missingMemberBehavior)),
            Name = name.Select(value => value.GetString()) ?? "",
            Range = range.Select(value => value.GetLeaderboardTierRange(missingMemberBehavior))
        };
    }
}
