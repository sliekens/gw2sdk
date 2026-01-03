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

        foreach (JsonProperty member in json.EnumerateObject())
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

        return new GuildRank
        {
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Order = order.Map(static (in value) => value.GetInt32()),
            Permissions =
                permissions.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetStringRequired())
                ),
            IconUrl = new Uri(iconHref.Map(static (in value) => value.GetStringRequired()))
        };
    }
}
