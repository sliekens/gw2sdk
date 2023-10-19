﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.MailCarriers;

[PublicAPI]
public static class MailCarrierJson
{
    public static MailCarrier GetMailCarrier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember unlockItems = new("unlock_items");
        RequiredMember order = new("order");
        RequiredMember icon = new("icon");
        RequiredMember name = new("name");
        RequiredMember flags = new("flags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(unlockItems.Name))
            {
                unlockItems = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MailCarrier
        {
            Id = id.Select(value => value.GetInt32()),
            UnlockItems = unlockItems.SelectMany(value => value.GetInt32()),
            Order = order.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired()),
            Flags = flags.SelectMany(value => value.GetEnum<MailCarrierFlag>(missingMemberBehavior))
        };
    }
}
