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
        OptionalMember color = "color";
        OptionalMember type = "type";
        OptionalMember name = "name";
        RequiredMember range = "range";

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
            Color = color.Map(value => value.GetString()) ?? "",
            Kind = type.Map(value => value.GetEnum<LeaderboardTierKind>(missingMemberBehavior)),
            Name = name.Map(value => value.GetString()) ?? "",
            Range = range.Map(value => value.GetLeaderboardTierRange(missingMemberBehavior))
        };
    }
}
