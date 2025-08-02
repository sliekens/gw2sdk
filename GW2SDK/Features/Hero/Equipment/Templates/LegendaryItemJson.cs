using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class LegendaryItemJson
{
    public static LegendaryItem GetLegendaryItem(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember maxCount = "max_count";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (maxCount.Match(member))
            {
                maxCount = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new LegendaryItem
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            MaxCount = maxCount.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
