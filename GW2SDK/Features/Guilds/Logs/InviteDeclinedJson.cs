using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class InviteDeclinedJson
{
    public static InviteDeclined GetInviteDeclined(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        OptionalMember declinedBy = "declined_by";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("invite_declined"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new InviteDeclined
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Time = time.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            User = user.Map(static (in JsonElement value) => value.GetStringRequired()),
            DeclinedBy = declinedBy.Map(static (in JsonElement value) => value.GetString()) ?? ""
        };
    }
}
