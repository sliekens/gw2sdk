using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
public static class SuperAdventureBoxZoneJson
{
    public static SuperAdventureBoxZone GetSuperAdventureBoxZone(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember mode = new("mode");
        RequiredMember world = new("world");
        RequiredMember zone = new("zone");

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
            Id = id.Select(value => value.GetInt32()),
            Mode = mode.Select(value => value.GetEnum<SuperAdventureBoxMode>(missingMemberBehavior)),
            World = world.Select(value => value.GetInt32()),
            Zone = zone.Select(value => value.GetInt32())
        };
    }
}
