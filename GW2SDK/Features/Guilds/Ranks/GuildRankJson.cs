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
        RequiredMember id = new("id");
        RequiredMember order = new("order");
        RequiredMember permissions = new("permissions");
        RequiredMember iconHref = new("icon");

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
            Id = id.Select(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            Permissions = permissions.SelectMany(value => value.GetEnum<GuildPermission>(missingMemberBehavior)),
            IconHref = iconHref.Select(value => value.GetStringRequired())
        };
    }
}
