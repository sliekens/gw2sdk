using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats;

[PublicAPI]
public static class SelectedStatJson
{
    public static SelectedStat GetSelectedStat(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SelectedModification> attributes = new("attributes");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedStat
        {
            Id = id.GetValue(),
            Attributes =
                attributes.Select(value => value.GetSelectedModification(missingMemberBehavior))
        };
    }
}
