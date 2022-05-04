using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats;

[PublicAPI]
public static class ItemStatReader
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
            Attributes = attributes.SelectMany(value => ReadAttribute(value, missingMemberBehavior))
        };
    }

    private static ItemStatAttribute ReadAttribute(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<UpgradeAttributeName> attribute = new("attribute");
        RequiredMember<double> multiplier = new("multiplier");
        RequiredMember<int> value = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(attribute.Name))
            {
                attribute.Value = member.Value;
            }
            else if (member.NameEquals(multiplier.Name))
            {
                multiplier.Value = member.Value;
            }
            else if (member.NameEquals(value.Name))
            {
                value.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemStatAttribute
        {
            Attribute = attribute.GetValue(missingMemberBehavior),
            Multiplier = multiplier.GetValue(),
            Value = value.GetValue()
        };
    }
}
