using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Raids;

[PublicAPI]
public static class RaidJson
{
    public static Raid GetRaid(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> id = new("id");
        RequiredMember<RaidWing> wings = new("wings");

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

        return new Raid
        {
            Id = id.GetValue(),
            Wings = wings.SelectMany(value => value.GetRaidWing(missingMemberBehavior))
        };
    }
}
