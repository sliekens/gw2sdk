using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildLogEntryJson
{
    public static GuildLogEntry GetGuildLogEntry(this JsonElement json)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "influence":
                    return json.GetInfluenceActivity();
                case "invite_declined":
                    return json.GetInviteDeclined();
                case "invited":
                    return json.GetMemberInvited();
                case "joined":
                    return json.GetMemberJoined();
                case "kick":
                    return json.GetMemberKicked();
                case "motd":
                    return json.GetNewMessageOfTheDay();
                case "rank_change":
                    return json.GetRankChange();
                case "stash":
                    return json.GetGuildBankActivity();
                case "treasury":
                    return json.GetTreasuryDeposit();
                case "upgrade":
                    return json.GetGuildUpgradeActivity();
            }
        }

        RequiredMember id = "id";
        RequiredMember time = "time";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildLogEntry
        {
            Id = id.Map(static value => value.GetInt32()),
            Time = time.Map(static value => value.GetDateTimeOffset())
        };
    }
}
