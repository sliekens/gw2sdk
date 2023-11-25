using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal static class MailCarrierJson
{
    public static MailCarrier GetMailCarrier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember flags = "flags";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == unlockItems.Name)
            {
                unlockItems = member;
            }
            else if (member.Name == order.Name)
            {
                order = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == flags.Name)
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
            Id = id.Map(value => value.GetInt32()),
            UnlockItems = unlockItems.Map(values => values.GetList(value => value.GetInt32())),
            Order = order.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired()),
            Flags = flags.Map(
                values => values.GetList(
                    value => value.GetEnum<MailCarrierFlag>(missingMemberBehavior)
                )
            )
        };
    }
}
