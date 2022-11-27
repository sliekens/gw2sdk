using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Amulets;

[PublicAPI]
public static class AmuletJson
{
    public static Amulet GetAmulet(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> icon = new("icon");
        RequiredMember<Dictionary<AttributeAdjustTarget, int>> attributes = new("attributes");

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
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
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

        return new Amulet
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            Icon = icon.GetValue(),
            Attributes = attributes.Select(value => value.GetAttributes(missingMemberBehavior))
        };
    }
}
