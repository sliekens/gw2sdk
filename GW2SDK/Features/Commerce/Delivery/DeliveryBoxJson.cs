using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Delivery;

[PublicAPI]
public static class DeliveryBoxJson
{
    public static DeliveryBox GetDeliveryBox(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<Coin> coins = new("coins");
        RequiredMember<DeliveredItem> items = new("items");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coins.Name))
            {
                coins.Value = member.Value;
            }
            else if (member.NameEquals(items.Name))
            {
                items.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DeliveryBox
        {
            Coins = coins.GetValue(),
            Items = items.SelectMany(item => item.GetDeliveredItem(missingMemberBehavior))
        };
    }
}
