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
        OptionalMember<string> color = new("color");
        OptionalMember<LeaderboardTierKind> type = new("type");
        OptionalMember<string> name = new("name");
        RequiredMember<LeaderboardTierRange> range = new("range");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(color.Name))
            {
                color.Value = member.Value;
            }
            else if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(range.Name))
            {
                range.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardTier
        {
            Color = color.GetValueOrEmpty(),
            Kind = type.GetValue(missingMemberBehavior),
            Name = name.GetValueOrEmpty(),
            Range = range.Select(value => value.GetLeaderboardTierRange(missingMemberBehavior))
        };
    }
}
