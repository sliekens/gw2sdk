using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Armory;

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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(maxCount.Name))
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
