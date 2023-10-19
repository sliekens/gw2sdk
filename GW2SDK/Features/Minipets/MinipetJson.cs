using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Minipets;

[PublicAPI]
public static class MinipetJson
{
    public static Minipet GetMinipet(
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(unlock.Name))
            {
                unlock = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Minipet
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Unlock = unlock.Map(value => value.GetString()) ?? "",
            Icon = icon.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
