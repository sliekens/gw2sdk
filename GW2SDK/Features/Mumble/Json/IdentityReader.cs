using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mumble.Json
{
    [PublicAPI]
    public static class IdentityReader
    {
        public static Identity Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var name = new RequiredMember<string>("name");
            var profession = new RequiredMember<ProfessionName>("profession");
            var specializationId = new RequiredMember<int>("spec");
            var race = new RequiredMember<Race>("race");
            var mapId = new RequiredMember<int>("map_id");
            var map = new RequiredMember<int>("map");
            var worldId = new RequiredMember<long>("world_id");
            var teamColorId = new RequiredMember<int>("team_color_id");
            var commander = new RequiredMember<bool>("commander");
            var fieldOfView = new RequiredMember<double>("fov");
            var uiSize = new RequiredMember<UiSize>("uisz");

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
                Profession = profession.Select(json => (ProfessionName) json.GetInt32()),
                SpecializationId = specializationId.GetValue(),
                Race = race.Select(json => (Race) (json.GetInt32() + 1)),
                MapId = mapId.GetValue(),
                WorldId = worldId.GetValue(),
                TeamColorId = teamColorId.GetValue(),
                Commander = commander.GetValue(),
                FieldOfView = fieldOfView.GetValue(),
                UiSize = uiSize.Select(json => (UiSize) json.GetInt32())
            };
        }
    }
}
