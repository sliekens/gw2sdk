﻿using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class AllWorldsJson
{
    public static AllWorlds GetAllWorlds(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> red = new("red");
        RequiredMember<int> blue = new("blue");
        RequiredMember<int> green = new("green");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(red.Name))
            {
                red.Value = member.Value;
            }
            else if (member.NameEquals(blue.Name))
            {
                blue.Value = member.Value;
            }
            else if (member.NameEquals(green.Name))
            {
                green.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AllWorlds
        {
            Red = red.GetValues(),
            Blue = blue.GetValues(),
            Green = green.GetValues()
        };
    }
}