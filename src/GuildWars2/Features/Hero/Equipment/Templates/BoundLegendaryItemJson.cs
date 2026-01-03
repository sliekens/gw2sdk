using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

internal static class BoundLegendaryItemJson
{
    public static BoundLegendaryItem GetBoundLegendaryItem(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember count = "count";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new BoundLegendaryItem
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Count = count.Map(static (in value) => value.GetInt32())
        };
    }
}
