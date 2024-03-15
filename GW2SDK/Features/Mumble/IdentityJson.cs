using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Mumble;

internal static class IdentityJson
{
    public static Identity GetIdentity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        if (missingMemberBehavior == MissingMemberBehavior.Error)
        {
            // The 'map' and 'map_id' seem to be redundant, but check my assumptions...
            if (map.Map(value => value.GetInt32()) != mapId.Map(value => value.GetInt32()))
            {
                throw new InvalidOperationException(Strings.UnexpectedMember("map"));
            }
        }

        return new Identity
        {
            Name = name.Map(value => value.GetStringRequired()),
            Profession = profession.Map(value => (ProfessionName)value.GetInt32()),
            SpecializationId = specializationId.Map(value => value.GetInt32()),
            Race = race.Map(value => (RaceName)(value.GetInt32() + 1)),
            MapId = mapId.Map(value => value.GetInt32()),
            WorldId = worldId.Map(value => value.GetInt64()),
            TeamColorId = teamColorId.Map(value => value.GetInt32()),
            Commander = commander.Map(value => value.GetBoolean()),
            FieldOfView = fieldOfView.Map(value => value.GetDouble()),
            UiSize = uiSize.Map(value => (UiSize)value.GetInt32())
        };
    }
}
