using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Delivery;

internal static class DeliveredItemJson
{
    public static DeliveredItem GetDeliveredItem(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DeliveredItem
        {
            Id = id.Map(static value => value.GetInt32()),
            Count = count.Map(static value => value.GetInt32())
        };
    }
}
