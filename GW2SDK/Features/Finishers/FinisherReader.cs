using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Finishers;

[PublicAPI]
public static class FinisherReader
{
    public static Finisher GetFinisher(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> unlockDetails = new("unlock_details");
        RequiredMember<int> unlockItems = new("unlock_items");
        RequiredMember<int> order = new("order");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> name = new("name");

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
            Id = id.GetValue(),
            UnlockDetails = unlockDetails.GetValue(),
            UnlockItems = unlockItems.SelectMany(entry => entry.GetInt32()),
            Order = order.GetValue(),
            Icon = icon.GetValue(),
            Name = name.GetValue()
        };
    }
}
