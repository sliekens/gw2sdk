using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class GuildLogJson
{
    public static GuildLog GetGuildLog(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "upgrade":
                return json.GetGuildUpgradeActivity(missingMemberBehavior);
            case "treasury":
                return json.GetTreasuryDeposit(missingMemberBehavior);
            case "stash":
                return json.GetStashActivity(missingMemberBehavior);
            case "rank_change":
                return json.GetRankChange(missingMemberBehavior);
            case "motd":
                return json.GetNewMessageOfTheDay(missingMemberBehavior);
            case "kick":
                return json.GetMemberKicked(missingMemberBehavior);
            case "joined":
                return json.GetMemberJoined(missingMemberBehavior);
            case "invited":
                return json.GetMemberInvited(missingMemberBehavior);
        }

        RequiredMember<int> id = new("id");
        RequiredMember<DateTimeOffset> time = new("time");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildLog
        {
            Id = id.GetValue(),
            Time = time.GetValue()
        };
    }
}
