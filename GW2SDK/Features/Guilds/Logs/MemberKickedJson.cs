using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class MemberKickedJson
{
    public static MemberKicked GetMemberKicked(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember time = new("time");
        RequiredMember user = new("user");
        RequiredMember kickedBy = new("kicked_by");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("kick"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(time.Name))
            {
                time.Value = member.Value;
            }
            else if (member.NameEquals(user.Name))
            {
                user.Value = member.Value;
            }
            else if (member.NameEquals(kickedBy.Name))
            {
                kickedBy.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MemberKicked
        {
            Id = id.Select(value => value.GetInt32()),
            Time = time.Select(value => value.GetDateTimeOffset()),
            User = user.Select(value => value.GetStringRequired()),
            KickedBy = kickedBy.Select(value => value.GetStringRequired())
        };
    }
}
