using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats;

[PublicAPI]
public static class ItemStatJson
{
    public static ItemStat GetItemStat(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<ItemStatAttribute> attributes = new("attributes");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
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

        return new ItemStat
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Attributes =
                attributes.SelectMany(value => value.GetItemStatAttribute(missingMemberBehavior))
        };
    }
}
