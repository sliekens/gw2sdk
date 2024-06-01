using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Delivery;

internal static class DeliveryBoxJson
{
    public static DeliveryBox GetDeliveryBox(this JsonElement json)
    {
        RequiredMember coins = "coins";
        RequiredMember items = "items";

        foreach (var member in json.EnumerateObject())
        {
            if (coins.Match(member))
            {
                coins = member;
            }
            else if (items.Match(member))
            {
                items = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new DeliveryBox
        {
            Coins = coins.Map(static value => value.GetInt32()),
            Items = items.Map(
                static values => values.GetList(static value => value.GetDeliveredItem())
            )
        };
    }
}
