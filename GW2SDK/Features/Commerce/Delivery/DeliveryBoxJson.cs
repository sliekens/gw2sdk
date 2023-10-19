using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Delivery;

[PublicAPI]
public static class DeliveryBoxJson
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
            if (member.NameEquals(coins.Name))
            {
                coins = member;
            }
            else if (member.NameEquals(items.Name))
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
            Coins = coins.Select(value => value.GetInt32()),
            Items = items.SelectMany(item => item.GetDeliveredItem(missingMemberBehavior))
        };
    }
}
