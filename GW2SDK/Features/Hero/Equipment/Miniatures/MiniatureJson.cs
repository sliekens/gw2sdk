using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Miniatures;

internal static class MiniatureJson
{
    public static Miniature GetMiniature(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Miniature
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            LockedText = unlock.Map(value => value.GetString()) ?? "",
            IconHref = icon.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
