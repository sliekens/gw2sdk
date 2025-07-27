using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Miniatures;

internal static class MiniatureJson
{
    public static Miniature GetMiniature(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        OptionalMember unlock = "unlock";
        RequiredMember icon = "icon";
        RequiredMember order = "order";
        RequiredMember itemId = "item_id";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (unlock.Match(member))
            {
                unlock = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        return new Miniature
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            LockedText = unlock.Map(static (in JsonElement value) => value.GetString()) ?? "",
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Order = order.Map(static (in JsonElement value) => value.GetInt32()),
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
