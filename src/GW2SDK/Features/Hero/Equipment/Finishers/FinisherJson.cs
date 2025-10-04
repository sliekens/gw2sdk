using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers;

internal static class FinisherJson
{
    public static Finisher GetFinisher(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember unlockDetails = "unlock_details";
        RequiredMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (unlockDetails.Match(member))
            {
                unlockDetails = member;
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in value) => value.GetStringRequired());
        return new Finisher
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            LockedText = unlockDetails.Map(static (in value) => value.GetStringRequired()),
            UnlockItemIds =
                unlockItems.Map(static (in values) => values.GetList(static (in value) => value.GetInt32())),
            Order = order.Map(static (in value) => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref assignment
            IconHref = iconString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            Name = name.Map(static (in value) => value.GetStringRequired())
        };
    }
}
