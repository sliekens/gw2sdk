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
        RequiredMember name = new("name");
        RequiredMember profession = new("profession");
        RequiredMember specializationId = new("spec");
        RequiredMember race = new("race");
        RequiredMember mapId = new("map_id");
        RequiredMember map = new("map");
        RequiredMember worldId = new("world_id");
        RequiredMember teamColorId = new("team_color_id");
        RequiredMember commander = new("commander");
        RequiredMember fieldOfView = new("fov");
        RequiredMember uiSize = new("uisz");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(specializationId.Name))
            {
                specializationId.Value = member.Value;
            }
            else if (member.NameEquals(race.Name))
            {
                race.Value = member.Value;
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId.Value = member.Value;
            }
            else if (member.NameEquals(worldId.Name))
            {
                worldId.Value = member.Value;
            }
            else if (member.NameEquals(teamColorId.Name))
            {
                teamColorId.Value = member.Value;
            }
            else if (member.NameEquals(commander.Name))
            {
                commander.Value = member.Value;
            }
            else if (member.NameEquals(fieldOfView.Name))
            {
                fieldOfView.Value = member.Value;
            }
            else if (member.NameEquals(uiSize.Name))
            {
                uiSize.Value = member.Value;
            }
            else if (member.NameEquals(map.Name))
            {
                map.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        if (missingMemberBehavior == MissingMemberBehavior.Error)
        {
            // The 'map' and 'map_id' seem to be redundant, but check my assumptions...
            if (map.Select(value => value.GetInt32()) != mapId.Select(value => value.GetInt32()))
            {
                throw new InvalidOperationException(Strings.UnexpectedMember("map"));
            }
        }

        return new Identity
        {
            Name = name.Select(value => value.GetStringRequired()),
            Profession = profession.Select(value => (ProfessionName)value.GetInt32()),
            SpecializationId = specializationId.Select(value => value.GetInt32()),
            Race = race.Select(value => (RaceName)(value.GetInt32() + 1)),
            MapId = mapId.Select(value => value.GetInt32()),
            WorldId = worldId.Select(value => value.GetInt64()),
            TeamColorId = teamColorId.Select(value => value.GetInt32()),
            Commander = commander.Select(value => value.GetBoolean()),
            FieldOfView = fieldOfView.Select(value => value.GetDouble()),
            UiSize = uiSize.Select(value => (UiSize)value.GetInt32())
        };
    }
}
