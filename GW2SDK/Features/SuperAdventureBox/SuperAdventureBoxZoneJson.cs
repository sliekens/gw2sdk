using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
public static class SuperAdventureBoxZoneJson
{
    public static SuperAdventureBoxZone GetSuperAdventureBoxZone(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<SuperAdventureBoxMode> mode = new("mode");
        RequiredMember<int> world = new("world");
        RequiredMember<int> zone = new("zone");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(mode.Name))
            {
                mode.Value = member.Value;
            }
            else if (member.NameEquals(world.Name))
            {
                world.Value = member.Value;
            }
            else if (member.NameEquals(zone.Name))
            {
                zone.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SuperAdventureBoxZone
        {
            Id = id.GetValue(),
            Mode = mode.GetValue(missingMemberBehavior),
            World = world.GetValue(),
            Zone = zone.GetValue()
        };
    }
}
