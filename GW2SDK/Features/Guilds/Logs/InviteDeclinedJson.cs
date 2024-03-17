using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class InviteDeclinedJson
{
    public static InviteDeclined GetInviteDeclined(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        RequiredMember declinedBy = "declined_by";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("invite_declined"))
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
            else if (declinedBy.Match(member))
            {
                declinedBy = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InviteDeclined
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset()),
            User = user.Map(value => value.GetStringRequired()),
            DeclinedBy = declinedBy.Map(value => value.GetStringRequired())
        };
    }
}
