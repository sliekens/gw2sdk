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
        RequiredMember name = new("name");
        RequiredMember rank = new("rank");
        NullableMember joined = new("joined");

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
            Name = name.Select(value => value.GetStringRequired()),
            Rank = rank.Select(value => value.GetStringRequired()),
            Joined = joined.Select(value => value.GetDateTimeOffset())
        };
    }
}
