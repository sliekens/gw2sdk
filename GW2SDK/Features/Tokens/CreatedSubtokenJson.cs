﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Tokens;

internal static class CreatedSubtokenJson
{
    public static CreatedSubtoken GetCreatedSubtoken(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember subtoken = "subtoken";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(subtoken.Name))
            {
                subtoken = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CreatedSubtoken { Subtoken = subtoken.Map(value => value.GetStringRequired()) };
    }
}
