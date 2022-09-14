﻿using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public static class BoundLegendaryItemReader
{
    public static BoundLegendaryItem GetBoundLegendaryItem(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");

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
            Id = id.GetValue(),
            Count = count.GetValue()
        };
    }
}