using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers;

internal static class FinisherJson
{
    public static Finisher GetFinisher(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember unlockDetails = "unlock_details";
        RequiredMember unlockItems = "unlock_items";
        RequiredMember order = "order";
        RequiredMember icon = "icon";
        RequiredMember name = "name";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == unlockDetails.Name)
            {
                unlockDetails = member;
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Finisher
        {
            Id = id.Map(value => value.GetInt32()),
            UnlockDetails = unlockDetails.Map(value => value.GetStringRequired()),
            UnlockItems = unlockItems.Map(values => values.GetList(entry => entry.GetInt32())),
            Order = order.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            Name = name.Map(value => value.GetStringRequired())
        };
    }
}
