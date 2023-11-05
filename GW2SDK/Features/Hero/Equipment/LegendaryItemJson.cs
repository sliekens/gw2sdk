using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class LegendaryItemJson
{
    public static LegendaryItem GetLegendaryItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember maxCount = "max_count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == maxCount.Name)
            {
                maxCount = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LegendaryItem
        {
            Id = id.Map(value => value.GetInt32()),
            MaxCount = maxCount.Map(value => value.GetInt32())
        };
    }
}
