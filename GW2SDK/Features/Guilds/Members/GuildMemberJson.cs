using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Members;

internal static class GuildMemberJson
{
    public static GuildMember GetGuildMember(this JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember rank = "rank";
        NullableMember joined = "joined";

        foreach (var member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildMember
        {
            Name = name.Map(static value => value.GetStringRequired()),
            Rank = rank.Map(static value => value.GetStringRequired()),
            Joined = joined.Map(static value => value.GetDateTimeOffset())
        };
    }
}
