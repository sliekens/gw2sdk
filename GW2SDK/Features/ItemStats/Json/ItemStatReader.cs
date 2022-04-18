using System;
using System.Text.Json;
using GW2SDK.ItemStats.Models;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats.Json;

[PublicAPI]
public static class ItemStatReader
{
    public static ItemStat Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<ItemStatAttribute> attributes = new("attributes");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes = attributes.From(member.Value);
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
                attribute = attribute.From(member.Value);
            }
            else if (member.NameEquals(multiplier.Name))
            {
                multiplier = multiplier.From(member.Value);
            }
            else if (member.NameEquals(value.Name))
            {
                value = value.From(member.Value);
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
