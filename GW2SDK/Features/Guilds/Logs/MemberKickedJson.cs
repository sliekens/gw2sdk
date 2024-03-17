using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class MemberKickedJson
{
    public static MemberKicked GetMemberKicked(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        RequiredMember kickedBy = "kicked_by";

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
            else if (id.Match(member))
            {
                id = member;
            }
            else if (time.Match(member))
            {
                time = member;
            }
            else if (user.Match(member))
            {
                user = member;
            }
            else if (kickedBy.Match(member))
            {
                kickedBy = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MemberKicked
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset()),
            User = user.Map(value => value.GetStringRequired()),
            KickedBy = kickedBy.Map(value => value.GetStringRequired())
        };
    }
}
