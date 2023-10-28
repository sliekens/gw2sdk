using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildLogJson
{
    public static GuildLog GetGuildLog(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "influence":
                return json.GetInfluenceActivity(missingMemberBehavior);
            case "invite_declined":
                return json.GetInviteDeclined(missingMemberBehavior);
            case "invited":
                return json.GetMemberInvited(missingMemberBehavior);
            case "joined":
                return json.GetMemberJoined(missingMemberBehavior);
            case "kick":
                return json.GetMemberKicked(missingMemberBehavior);
            case "motd":
                return json.GetNewMessageOfTheDay(missingMemberBehavior);
            case "rank_change":
                return json.GetRankChange(missingMemberBehavior);
            case "stash":
                return json.GetStashActivity(missingMemberBehavior);
            case "treasury":
                return json.GetTreasuryDeposit(missingMemberBehavior);
            case "upgrade":
                return json.GetGuildUpgradeActivity(missingMemberBehavior);
        }

        RequiredMember id = "id";
        RequiredMember time = "time";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == time.Name)
            {
                time = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildLog
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset())
        };
    }
}
