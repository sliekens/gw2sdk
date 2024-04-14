using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers;

internal static class FinisherJson
{
    public static Finisher GetFinisher(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember unlockDetails = "unlock_details";
        RequiredMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";

        foreach (var member in json.EnumerateObject())
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Finisher
        {
            Id = id.Map(static value => value.GetInt32()),
            LockedText = unlockDetails.Map(static value => value.GetStringRequired()),
            UnlockItemIds =
                unlockItems.Map(static values => values.GetList(static value => value.GetInt32())),
            Order = order.Map(static value => value.GetInt32()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            Name = name.Map(static value => value.GetStringRequired())
        };
    }
}
