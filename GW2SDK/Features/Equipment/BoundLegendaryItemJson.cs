using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Equipment;

internal static class BoundLegendaryItemJson
{
    public static BoundLegendaryItem GetBoundLegendaryItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == count.Name)
            {
                count = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BoundLegendaryItem
        {
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}
