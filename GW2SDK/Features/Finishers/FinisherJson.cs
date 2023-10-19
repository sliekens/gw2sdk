using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Finishers;

[PublicAPI]
public static class FinisherJson
{
    public static Finisher GetFinisher(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember unlockDetails = new("unlock_details");
        RequiredMember unlockItems = new("unlock_items");
        RequiredMember order = new("order");
        RequiredMember icon = new("icon");
        RequiredMember name = new("name");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(unlockDetails.Name))
            {
                unlockDetails.Value = member.Value;
            }
            else if (member.NameEquals(unlockItems.Name))
            {
                unlockItems.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Finisher
        {
            Id = id.Select(value => value.GetInt32()),
            UnlockDetails = unlockDetails.Select(value => value.GetStringRequired()),
            UnlockItems = unlockItems.SelectMany(entry => entry.GetInt32()),
            Order = order.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetStringRequired()),
            Name = name.Select(value => value.GetStringRequired())
        };
    }
}
