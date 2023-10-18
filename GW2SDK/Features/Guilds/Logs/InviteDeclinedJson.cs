using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class InviteDeclinedJson
{
    public static InviteDeclined GetInviteDeclined(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<DateTimeOffset> time = new("time");
        RequiredMember<string> user = new("user");
        RequiredMember<string> declinedBy = new("declined_by");

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
            else if (member.NameEquals(declinedBy.Name))
            {
                declinedBy.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InviteDeclined
        {
            Id = id.GetValue(),
            Time = time.GetValue(),
            User = user.GetValue(),
            DeclinedBy = declinedBy.GetValue()
        };
    }
}
