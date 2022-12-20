﻿using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Raids;

[PublicAPI]
public static class RaidWingEventJson
{
    public static RaidWingEvent GetRaidWingEvent(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<RaidWingEventKind> kind = new("type");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(kind.Name))
            {
                kind.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RaidWingEvent
        {
            Id = id.GetValue(),
            Kind = kind.GetValue(missingMemberBehavior)
        };
    }
}