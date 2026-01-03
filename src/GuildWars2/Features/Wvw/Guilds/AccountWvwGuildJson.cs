using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Guilds;

internal static class AccountWvwGuildJson
{
    public static AccountWvwGuild GetAccountWvwGuild(this in JsonElement json)
    {
        NullableMember teamId = "team";
        OptionalMember guildId = "guild";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (teamId.Match(member))
            {
                teamId = member;
            }
            else if (guildId.Match(member))
            {
                guildId = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        int? teamIdValue = teamId.Map(static (in value) => value.GetInt32());
        return new AccountWvwGuild
        {
            TeamId = teamIdValue > 0 ? teamIdValue : null,
            GuildId = guildId.Map(static (in value) => value.GetString())
        };
    }
}
