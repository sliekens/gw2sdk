﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Raids;

[PublicAPI]
public static class RaidJson
{
    public static Raid GetRaid(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember wings = "wings";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(wings.Name))
            {
                wings = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Raid
        {
            Id = id.Map(value => value.GetStringRequired()),
            Wings = wings.Map(values => values.GetList(value => value.GetRaidWing(missingMemberBehavior)))
        };
    }
}
