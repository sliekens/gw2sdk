using System.Text.Json;
using GuildWars2.Hero;
using GuildWars2.Json;

namespace GuildWars2.Mumble;

internal static class IdentityJson
{
    public static Identity GetIdentity(this JsonElement json)
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
        {
            // The 'map' and 'map_id' seem to be redundant, but check my assumptions...
            if (map.Map(static value => value.GetInt32())
                != mapId.Map(static value => value.GetInt32()))
            {
                throw new InvalidOperationException(Strings.UnexpectedMember("map"));
            }
        }

        return new Identity
        {
            Name = name.Map(static value => value.GetStringRequired()),
            Profession = profession.Map(static value => (ProfessionName)value.GetInt32()),
            SpecializationId = specializationId.Map(static value => value.GetInt32()),
            Race = race.Map(static value => (RaceName)(value.GetInt32() + 1)),
            MapId = mapId.Map(static value => value.GetInt32()),
            WorldId = worldId.Map(static value => value.GetInt64()),
            TeamColorId = teamColorId.Map(static value => value.GetInt32()),
            Commander = commander.Map(static value => value.GetBoolean()),
            FieldOfView = fieldOfView.Map(static value => value.GetDouble()),
            UiSize = uiSize.Map(static value => (UiSize)value.GetInt32())
        };
    }
}
