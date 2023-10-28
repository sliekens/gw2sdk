using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Maps;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember minLevel = "min_level";
        RequiredMember maxLevel = "max_level";
        RequiredMember defaultFloor = "default_floor";
        RequiredMember kind = "type";
        RequiredMember floors = "floors";
        NullableMember regionId = "region_id";
        OptionalMember regionName = "region_name";
        NullableMember continentId = "continent_id";
        OptionalMember continentName = "continent_name";
        RequiredMember mapRectangle = "map_rect";
        RequiredMember continentRectangle = "continent_rect";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == minLevel.Name)
            {
                minLevel = member;
            }
            else if (member.Name == maxLevel.Name)
            {
                maxLevel = member;
            }
            else if (member.Name == defaultFloor.Name)
            {
                defaultFloor = member;
            }
            else if (member.Name == kind.Name)
            {
                kind = member;
            }
            else if (member.Name == floors.Name)
            {
                floors = member;
            }
            else if (member.Name == regionId.Name)
            {
                regionId = member;
            }
            else if (member.Name == regionName.Name)
            {
                regionName = member;
            }
            else if (member.Name == continentId.Name)
            {
                continentId = member;
            }
            else if (member.Name == continentName.Name)
            {
                continentName = member;
            }
            else if (member.Name == mapRectangle.Name)
            {
                mapRectangle = member;
            }
            else if (member.Name == continentRectangle.Name)
            {
                continentRectangle = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MapSummary
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            MinLevel = minLevel.Map(value => value.GetInt32()),
            MaxLevel = maxLevel.Map(value => value.GetInt32()),
            DefaultFloor = defaultFloor.Map(value => value.GetInt32()),
            Kind = kind.Map(value => value.GetEnum<MapKind>(missingMemberBehavior)),
            Floors = floors.Map(values => values.GetList(value => value.GetInt32())),
            RegionId = regionId.Map(value => value.GetInt32()),
            RegionName = regionName.Map(value => value.GetString()) ?? "",
            ContinentId = continentId.Map(value => value.GetInt32()),
            ContinentName = continentName.Map(value => value.GetString()) ?? "",
            MapRectangle = mapRectangle.Map(value => value.GetMapRectangle(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Map(value => value.GetContinentRectangle(missingMemberBehavior))
        };
    }
}
