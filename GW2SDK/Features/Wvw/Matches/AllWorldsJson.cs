﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class AllWorldsJson
{
    public static AllWorlds GetAllWorlds(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember red = "red";
        RequiredMember blue = "blue";
        RequiredMember green = "green";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(red.Name))
            {
                red = member;
            }
            else if (member.NameEquals(blue.Name))
            {
                blue = member;
            }
            else if (member.NameEquals(green.Name))
            {
                green = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AllWorlds
        {
            Red = red.Map(values => values.GetList(value => value.GetInt32())),
            Blue = blue.Map(values => values.GetList(value => value.GetInt32())),
            Green = green.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
