using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal static class MailCarrierJson
{
    public static MailCarrier GetMailCarrier(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";
        RequiredMember flags = "flags";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        return new MailCarrier
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            UnlockItemIds =
                unlockItems.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())),
            Order = order.Map(static (in JsonElement value) => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Flags = flags.Map(static (in JsonElement values) => values.GetMailCarrierFlags())
        };
    }
}
