using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce.Delivery.Json;

[PublicAPI]
public static class DeliveryBoxReader
{
    public static DeliveryBox Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<Coin> coins = new("coins");
        RequiredMember<DeliveredItem> items = new("items");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(coins.Name))
            {
                coins = coins.From(member.Value);
            }
            else if (member.NameEquals(items.Name))
            {
                items = items.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DeliveryBox
        {
            Coins = coins.GetValue(),
            Items = items.SelectMany(item => ReadDeliveredItem(item, missingMemberBehavior))
        };
    }

    private static DeliveredItem ReadDeliveredItem(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(count.Name))
            {
                count = count.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DeliveredItem
        {
            Id = id.GetValue(),
            Count = count.GetValue()
        };
    }
}
