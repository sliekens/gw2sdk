using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class NewMessageOfTheDayJson
{
    public static NewMessageOfTheDay GetNewMessageOfTheDay(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember time = new("time");
        RequiredMember user = new("user");
        RequiredMember motd = new("motd");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("motd"))
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
            else if (member.NameEquals(motd.Name))
            {
                motd.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new NewMessageOfTheDay
        {
            Id = id.Select(value => value.GetInt32()),
            Time = time.Select(value => value.GetDateTimeOffset()),
            User = user.Select(value => value.GetStringRequired()),
            MessageOfTheDay = motd.Select(value => value.GetStringRequired())
        };
    }
}
