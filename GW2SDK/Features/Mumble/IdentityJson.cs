using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Mumble;

[PublicAPI]
public static class IdentityJson
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
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = member;
            }
            else if (member.NameEquals(specializationId.Name))
            {
                specializationId = member;
            }
            else if (member.NameEquals(race.Name))
            {
                race = member;
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId = member;
            }
            else if (member.NameEquals(worldId.Name))
            {
                worldId = member;
            }
            else if (member.NameEquals(teamColorId.Name))
            {
                teamColorId = member;
            }
            else if (member.NameEquals(commander.Name))
            {
                commander = member;
            }
            else if (member.NameEquals(fieldOfView.Name))
            {
                fieldOfView = member;
            }
            else if (member.NameEquals(uiSize.Name))
            {
                uiSize = member;
            }
            else if (member.NameEquals(map.Name))
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
