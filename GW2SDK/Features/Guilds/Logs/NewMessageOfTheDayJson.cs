using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class NewMessageOfTheDayJson
{
    public static NewMessageOfTheDay GetNewMessageOfTheDay(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        RequiredMember user = "user";
        RequiredMember motd = "motd";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("motd"))
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
            else if (motd.Match(member))
            {
                motd = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new NewMessageOfTheDay
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Time = time.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            User = user.Map(static (in JsonElement value) => value.GetStringRequired()),
            MessageOfTheDay = motd.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
