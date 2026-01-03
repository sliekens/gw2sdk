using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Members;

internal static class GuildMemberJson
{
    public static GuildMember GetGuildMember(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember rank = "rank";
        RequiredMember wvwMember = "wvw_member";
        NullableMember joined = "joined";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (rank.Match(member))
            {
                rank = member;
            }
            else if (joined.Match(member))
            {
                joined = member;
            }
            else if (wvwMember.Match(member))
            {
                wvwMember = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildMember
        {
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Rank = rank.Map(static (in value) => value.GetStringRequired()),
            Joined = joined.Map(static (in value) => value.GetDateTimeOffset()),
            WvwMember = wvwMember.Map(static (in value) => value.GetBoolean())
        };
    }
}
