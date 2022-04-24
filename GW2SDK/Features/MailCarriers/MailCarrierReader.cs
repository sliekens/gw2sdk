using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.MailCarriers;

[PublicAPI]
public static class MailCarrierReader
{
    public static MailCarrier Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> unlockItems = new("unlock_items");
        RequiredMember<int> order = new("order");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");
        RequiredMember<MailCarrierFlag> flags = new("flags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(unlockItems.Name))
            {
                unlockItems = unlockItems.From(member.Value);
            }
            else if (member.NameEquals(order.Name))
            {
                order = order.From(member.Value);
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = icon.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MailCarrier
        {
            Id = id.GetValue(),
            UnlockItems = unlockItems.SelectMany(value => value.GetInt32()),
            Order = order.GetValue(),
            Icon = icon.GetValue(),
            Name = name.GetValue(),
            Flags = flags.GetValues(missingMemberBehavior)
        };
    }
}
