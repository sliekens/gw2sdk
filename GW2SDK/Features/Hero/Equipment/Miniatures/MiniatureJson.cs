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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == unlock.Name)
            {
                unlock = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == itemId.Name)
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
            Unlock = unlock.Map(value => value.GetString()) ?? "",
            IconHref = icon.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
