using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Ranks;

internal static class GuildRankJson
{
    public static GuildRank GetGuildRank(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember order = "order";
        RequiredMember permissions = "permissions";
        RequiredMember iconHref = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (permissions.Match(member))
            {
                permissions = member;
            }
            else if (iconHref.Match(member))
            {
                iconHref = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = iconHref.Map(static (in JsonElement value) => value.GetStringRequired());
        return new GuildRank
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Order = order.Map(static (in JsonElement value) => value.GetInt32()),
            Permissions =
                permissions.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetStringRequired())
                ),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute)
        };
    }
}
