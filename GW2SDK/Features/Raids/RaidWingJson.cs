using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Raids;

[PublicAPI]
public static class RaidWingJson
{
    public static RaidWing GetRaidWing(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<RaidWingEvent> wings = new("events");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(wings.Name))
            {
                wings.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RaidWing
        {
            Id = id.GetValue(),
            Events = wings.SelectMany(value => value.GetRaidWingEvent(missingMemberBehavior))
        };
    }
}
