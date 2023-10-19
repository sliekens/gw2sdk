using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Armory;

[PublicAPI]
public static class BoundLegendaryItemJson
{
    public static BoundLegendaryItem GetBoundLegendaryItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember count = new("count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BoundLegendaryItem
        {
            Id = id.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
