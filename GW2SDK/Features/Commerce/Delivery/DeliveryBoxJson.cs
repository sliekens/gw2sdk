using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Delivery;

internal static class DeliveryBoxJson
{
    public static DeliveryBox GetDeliveryBox(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DeliveryBox
        {
            Coins = coins.Map(value => value.GetInt32()),
            Items = items.Map(
                values => values.GetList(item => item.GetDeliveredItem(missingMemberBehavior))
            )
        };
    }
}
