using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.SuperAdventureBox;

internal static class SuperAdventureBoxZoneJson
{
    public static SuperAdventureBoxZone GetSuperAdventureBoxZone(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember mode = "mode";
        RequiredMember world = "world";
        RequiredMember zone = "zone";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (mode.Match(member))
            {
                mode = member;
            }
            else if (world.Match(member))
            {
                world = member;
            }
            else if (zone.Match(member))
            {
                zone = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SuperAdventureBoxZone
        {
            Id = id.Map(static value => value.GetInt32()),
            Mode = mode.Map(
                static value =>
                    value.ValueEquals("infantile") // Infantile was renamed to Exploration
                        ? SuperAdventureBoxMode.Exploration
                        : value.GetEnum<SuperAdventureBoxMode>()
            ),
            World = world.Map(static value => value.GetInt32()),
            Zone = zone.Map(static value => value.GetInt32())
        };
    }
}
