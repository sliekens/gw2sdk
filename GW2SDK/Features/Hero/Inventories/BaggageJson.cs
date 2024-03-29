﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal static class BaggageJson
{
    public static Baggage GetBaggage(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember bags = "bags";

        foreach (var member in json.EnumerateObject())
        {
            if (bags.Match(member))
            {
                bags = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Baggage
        {
            Bags = bags.Map(
                values => values.GetList(value => value.GetBag(missingMemberBehavior))
            )
        };
    }
}
