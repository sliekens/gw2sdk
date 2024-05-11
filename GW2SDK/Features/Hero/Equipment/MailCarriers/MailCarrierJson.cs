using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal static class MailCarrierJson
{
    public static MailCarrier GetMailCarrier(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember flags = "flags";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (unlockItems.Match(member))
            {
                unlockItems = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MailCarrier
        {
            Id = id.Map(static value => value.GetInt32()),
            UnlockItemIds =
                unlockItems.Map(static values => values.GetList(static value => value.GetInt32())),
            Order = order.Map(static value => value.GetInt32()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired()),
            Flags = flags.Map(static values => values.GetMailCarrierFlags())
        };
    }
}
