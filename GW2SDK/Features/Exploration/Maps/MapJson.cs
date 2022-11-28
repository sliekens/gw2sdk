using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Exploration.Maps;

[PublicAPI]
public static class MapJson
{
    public static Map GetMap(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> minLevel = new("min_level");
        RequiredMember<int> maxLevel = new("max_level");
        RequiredMember<int> defaultFloor = new("default_floor");
        RequiredMember<MapKind> kind = new("type");
        RequiredMember<int> floors = new("floors");
        NullableMember<int> regionId = new("region_id");
        OptionalMember<string> regionName = new("region_name");
        NullableMember<int> continentId = new("continent_id");
        OptionalMember<string> continentName = new("continent_name");
        RequiredMember<MapArea> mapRectangle = new("map_rect");
        RequiredMember<Area> continentRectangle = new("continent_rect");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(minLevel.Name))
            {
                minLevel.Value = member.Value;
            }
            else if (member.NameEquals(maxLevel.Name))
            {
                maxLevel.Value = member.Value;
            }
            else if (member.NameEquals(defaultFloor.Name))
            {
                defaultFloor.Value = member.Value;
            }
            else if (member.NameEquals(kind.Name))
            {
                kind.Value = member.Value;
            }
            else if (member.NameEquals(floors.Name))
            {
                floors.Value = member.Value;
            }
            else if (member.NameEquals(regionId.Name))
            {
                regionId.Value = member.Value;
            }
            else if (member.NameEquals(regionName.Name))
            {
                regionName.Value = member.Value;
            }
            else if (member.NameEquals(continentId.Name))
            {
                continentId.Value = member.Value;
            }
            else if (member.NameEquals(continentName.Name))
            {
                continentName.Value = member.Value;
            }
            else if (member.NameEquals(mapRectangle.Name))
            {
                mapRectangle.Value = member.Value;
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Map
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            MinLevel = minLevel.GetValue(),
            MaxLevel = maxLevel.GetValue(),
            DefaultFloor = defaultFloor.GetValue(),
            Kind = kind.GetValue(missingMemberBehavior),
            Floors = floors.SelectMany(value => value.GetInt32()),
            RegionId = regionId.GetValue(),
            RegionName = regionName.GetValueOrEmpty(),
            ContinentId = continentId.GetValue(),
            ContinentName = continentName.GetValueOrEmpty(),
            MapRectangle = mapRectangle.Select(value => value.GetMapArea(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => value.GetArea(missingMemberBehavior))
        };
    }
}
