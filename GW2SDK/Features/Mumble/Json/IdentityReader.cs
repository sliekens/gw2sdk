using System;
using System.Text.Json;

using GW2SDK.Json;
using GW2SDK.Mumble.Models;

using JetBrains.Annotations;

namespace GW2SDK.Mumble.Json;

[PublicAPI]
public static class IdentityReader
{
    public static Identity Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<int> specializationId = new("spec");
        RequiredMember<Race> race = new("race");
        RequiredMember<int> mapId = new("map_id");
        RequiredMember<int> map = new("map");
        RequiredMember<long> worldId = new("world_id");
        RequiredMember<int> teamColorId = new("team_color_id");
        RequiredMember<bool> commander = new("commander");
        RequiredMember<double> fieldOfView = new("fov");
        RequiredMember<UiSize> uiSize = new("uisz");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = profession.From(member.Value);
            }
            else if (member.NameEquals(specializationId.Name))
            {
                specializationId = specializationId.From(member.Value);
            }
            else if (member.NameEquals(race.Name))
            {
                race = race.From(member.Value);
            }
            else if (member.NameEquals(mapId.Name))
            {
                mapId = mapId.From(member.Value);
            }
            else if (member.NameEquals(worldId.Name))
            {
                worldId = worldId.From(member.Value);
            }
            else if (member.NameEquals(teamColorId.Name))
            {
                teamColorId = teamColorId.From(member.Value);
            }
            else if (member.NameEquals(commander.Name))
            {
                commander = commander.From(member.Value);
            }
            else if (member.NameEquals(fieldOfView.Name))
            {
                fieldOfView = fieldOfView.From(member.Value);
            }
            else if (member.NameEquals(uiSize.Name))
            {
                uiSize = uiSize.From(member.Value);
            }
            else if (member.NameEquals(map.Name))
            {
                map = map.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        if (missingMemberBehavior == MissingMemberBehavior.Error)
        {
            // The 'map' and 'map_id' seem to be redundant, but check my assumptions...
            if (map.GetValue() != mapId.GetValue())
            {
                throw new InvalidOperationException(Strings.UnexpectedMember("map"));
            }
        }

        return new Identity
        {
            Name = name.GetValue(),
            Profession = profession.Select(json => (ProfessionName)json.GetInt32()),
            SpecializationId = specializationId.GetValue(),
            Race = race.Select(json => (Race)(json.GetInt32() + 1)),
            MapId = mapId.GetValue(),
            WorldId = worldId.GetValue(),
            TeamColorId = teamColorId.GetValue(),
            Commander = commander.GetValue(),
            FieldOfView = fieldOfView.GetValue(),
            UiSize = uiSize.Select(json => (UiSize)json.GetInt32())
        };
    }
}