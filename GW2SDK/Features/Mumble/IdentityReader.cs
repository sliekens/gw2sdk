﻿using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mumble;

[PublicAPI]
public static class IdentityReader
{
    public static Identity GetIdentity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<int> specializationId = new("spec");
        RequiredMember<RaceName> race = new("race");
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
            if (map.GetValue() != mapId.GetValue())
            {
                throw new InvalidOperationException(Strings.UnexpectedMember("map"));
            }
        }

        return new Identity
        {
            Name = name.GetValue(),
            Profession = profession.Select(value => (ProfessionName)value.GetInt32()),
            SpecializationId = specializationId.GetValue(),
            Race = race.Select(value => (RaceName)(value.GetInt32() + 1)),
            MapId = mapId.GetValue(),
            WorldId = worldId.GetValue(),
            TeamColorId = teamColorId.GetValue(),
            Commander = commander.GetValue(),
            FieldOfView = fieldOfView.GetValue(),
            UiSize = uiSize.Select(value => (UiSize)value.GetInt32())
        };
    }
}
