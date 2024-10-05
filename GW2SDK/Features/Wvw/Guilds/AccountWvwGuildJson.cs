using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Guilds;

internal static class AccountWvwGuildJson
{
    public static AccountWvwGuild GetAccountWvwGuild(this JsonElement json)
    {
        NullableMember teamId = "team";
        OptionalMember guildId = "guild";

        foreach (var member in json.EnumerateObject())
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

        return new AccountWvwGuild
        {
            TeamId = teamId.Map(static value => value.GetInt32()),
            GuildId = guildId.Map(static value => value.GetString())
        };
    }
}
