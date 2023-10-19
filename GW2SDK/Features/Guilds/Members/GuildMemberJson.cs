using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Members;

[PublicAPI]
public static class GuildMemberJson
{
    public static GuildMember GetGuildMember(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember rank = "rank";
        NullableMember joined = "joined";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(rank.Name))
            {
                rank = member;
            }
            else if (member.NameEquals(joined.Name))
            {
                joined = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildMember
        {
            Name = name.Map(value => value.GetStringRequired()),
            Rank = rank.Map(value => value.GetStringRequired()),
            Joined = joined.Map(value => value.GetDateTimeOffset())
        };
    }
}
