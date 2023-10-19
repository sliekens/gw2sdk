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
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        OptionalMember unlock = new("unlock");
        RequiredMember icon = new("icon");
        RequiredMember order = new("order");
        RequiredMember itemId = new("item_id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(unlock.Name))
            {
                unlock.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Minipet
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Unlock = unlock.Select(value => value.GetString()) ?? "",
            Icon = icon.Select(value => value.GetStringRequired()),
            Order = order.Select(value => value.GetInt32()),
            ItemId = itemId.Select(value => value.GetInt32())
        };
    }
}
