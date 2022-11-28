using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Minipets;

[PublicAPI]
public static class MinipetJson
{
    public static Minipet GetMinipet(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        OptionalMember<string> unlock = new("unlock");
        RequiredMember<string> icon = new("icon");
        RequiredMember<int> order = new("order");
        RequiredMember<int> itemId = new("item_id");

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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Unlock = unlock.GetValueOrEmpty(),
            Icon = icon.GetValue(),
            Order = order.GetValue(),
            ItemId = itemId.GetValue()
        };
    }
}
