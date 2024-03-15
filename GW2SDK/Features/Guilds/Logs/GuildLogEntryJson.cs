using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildLogEntryJson
{
    public static GuildLogEntry GetGuildLogEntry(
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
                return json.GetGuildBankActivity(missingMemberBehavior);
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
            else if (id.Match(member))
            {
                id = member;
            }
            else if (time.Match(member))
            {
                time = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildLogEntry
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset())
        };
    }
}
