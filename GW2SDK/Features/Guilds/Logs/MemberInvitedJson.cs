using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class MemberInvitedJson
{
    public static MemberInvited GetMemberInvited(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        RequiredMember invitedBy = "invited_by";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("invited"))
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
            else if (invitedBy.Match(member))
            {
                invitedBy = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MemberInvited
        {
            Id = id.Map(static value => value.GetInt32()),
            Time = time.Map(static value => value.GetDateTimeOffset()),
            User = user.Map(static value => value.GetStringRequired()),
            InvitedBy = invitedBy.Map(static value => value.GetStringRequired())
        };
    }
}
