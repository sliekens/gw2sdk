using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Commerce.Delivery;

internal static class DeliveredItemJson
{
    public static DeliveredItem GetDeliveredItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DeliveredItem
        {
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}
