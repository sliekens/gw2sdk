using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Ranks;

internal static class GuildRankJson
{
    public static GuildRank GetGuildRank(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember order = "order";
        RequiredMember permissions = "permissions";
        RequiredMember iconHref = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(permissions.Name))
            {
                permissions = member;
            }
            else if (member.NameEquals(iconHref.Name))
            {
                iconHref = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildRank
        {
            Id = id.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            Permissions =
                permissions.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<GuildPermission>(missingMemberBehavior)
                        )
                ),
            IconHref = iconHref.Map(value => value.GetStringRequired())
        };
    }
}
