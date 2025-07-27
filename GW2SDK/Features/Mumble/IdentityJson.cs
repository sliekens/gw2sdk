using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Mumble;

internal static class IdentityJson
{
    public static Identity GetIdentity(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember profession = "profession";
        RequiredMember specializationId = "spec";
        RequiredMember race = "race";
        RequiredMember mapId = "map_id";
        RequiredMember map = "map";
        RequiredMember worldId = "world_id";
        RequiredMember teamColorId = "team_color_id";
        RequiredMember commander = "commander";
        RequiredMember fieldOfView = "fov";
        RequiredMember uiSize = "uisz";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (profession.Match(member))
            {
                profession = member;
            }
            else if (specializationId.Match(member))
            {
                specializationId = member;
            }
            else if (race.Match(member))
            {
                race = member;
            }
            else if (mapId.Match(member))
            {
                mapId = member;
            }
            else if (worldId.Match(member))
            {
                worldId = member;
            }
            else if (teamColorId.Match(member))
            {
                teamColorId = member;
            }
            else if (commander.Match(member))
            {
                commander = member;
            }
            else if (fieldOfView.Match(member))
            {
                fieldOfView = member;
            }
            else if (uiSize.Match(member))
            {
                uiSize = member;
            }
            else if (map.Match(member))
            {
                map = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var mapIdInt32 = mapId.Map(static (in JsonElement value) => value.GetInt32());
        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
        {
            // The 'map_id' and 'map' seem to be redundant, but check my assumptions...
            if (mapIdInt32 != map.Map(static (in JsonElement value) => value.GetInt32()))
            {
                ThrowHelper.ThrowUnexpectedMember("map");
            }
        }

        return new Identity
        {
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Profession = profession.Map(static (in JsonElement value) => (ProfessionName)value.GetInt32()),
            SpecializationId = specializationId.Map(static (in JsonElement value) => value.GetInt32()),
            Race = race.Map(static (in JsonElement value) => (RaceName)(value.GetInt32() + 1)),
            MapId = mapIdInt32,
            WorldId = worldId.Map(static (in JsonElement value) => value.GetInt64()),
            TeamColorId = teamColorId.Map(static (in JsonElement value) => value.GetInt32()),
            Commander = commander.Map(static (in JsonElement value) => value.GetBoolean()),
            FieldOfView = fieldOfView.Map(static (in JsonElement value) => value.GetDouble()),
            UiSize = uiSize.Map(static (in JsonElement value) => (UiSize)value.GetInt32())
        };
    }
}
