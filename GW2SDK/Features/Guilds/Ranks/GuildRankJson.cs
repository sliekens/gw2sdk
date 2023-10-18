using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Ranks;

[PublicAPI]
public static class GuildRankJson
{
    public static GuildRank GetGuildRank(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<int> order = new("order");
        RequiredMember<GuildPermission> permissions = new("permissions");
        RequiredMember<string> iconHref = new("icon");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(permissions.Name))
            {
                permissions.Value = member.Value;
            }
            else if (member.NameEquals(iconHref.Name))
            {
                iconHref.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildRank
        {
            Id = id.GetValue(),
            Order = order.GetValue(),
            Permissions = permissions.GetValues(missingMemberBehavior),
            IconHref = iconHref.GetValue()
        };
    }
}
