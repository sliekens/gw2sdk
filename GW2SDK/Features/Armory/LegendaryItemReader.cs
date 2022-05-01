using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public static class LegendaryItemReader
{
    public static LegendaryItem GetLegendaryItem(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> maxCount = new("max_count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(maxCount.Name))
            {
                maxCount = maxCount.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LegendaryItem
        {
            Id = id.GetValue(),
            MaxCount = maxCount.GetValue()
        };
    }
}
