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
        RequiredMember<string> name = new("name");
        RequiredMember<string> rank = new("rank");
        NullableMember<DateTimeOffset> joined = new("joined");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(rank.Name))
            {
                rank.Value = member.Value;
            }
            else if (member.NameEquals(joined.Name))
            {
                joined.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildMember
        {
            Name = name.GetValue(),
            Rank = rank.GetValue(),
            Joined = joined.GetValue()
        };
    }
}
