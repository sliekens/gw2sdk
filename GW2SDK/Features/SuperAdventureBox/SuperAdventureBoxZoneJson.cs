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
        RequiredMember id = "id";
        RequiredMember mode = "mode";
        RequiredMember world = "world";
        RequiredMember zone = "zone";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(mode.Name))
            {
                mode = member;
            }
            else if (member.NameEquals(world.Name))
            {
                world = member;
            }
            else if (member.NameEquals(zone.Name))
            {
                zone = member;
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
