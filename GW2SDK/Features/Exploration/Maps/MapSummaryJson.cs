using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Maps;

internal static class MapSummaryJson
{
    public static MapSummary GetMapSummary(this JsonElement json)
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (minLevel.Match(member))
            {
                minLevel = member;
            }
            else if (maxLevel.Match(member))
            {
                maxLevel = member;
            }
            else if (defaultFloor.Match(member))
            {
                defaultFloor = member;
            }
            else if (kind.Match(member))
            {
                kind = member;
            }
            else if (floors.Match(member))
            {
                floors = member;
            }
            else if (regionId.Match(member))
            {
                regionId = member;
            }
            else if (regionName.Match(member))
            {
                regionName = member;
            }
            else if (continentId.Match(member))
            {
                continentId = member;
            }
            else if (continentName.Match(member))
            {
                continentName = member;
            }
            else if (mapRectangle.Match(member))
            {
                mapRectangle = member;
            }
            else if (continentRectangle.Match(member))
            {
                continentRectangle = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MapSummary
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            MinLevel = minLevel.Map(static value => value.GetInt32()),
            MaxLevel = maxLevel.Map(static value => value.GetInt32()),
            DefaultFloor = defaultFloor.Map(static value => value.GetInt32()),
            Kind = kind.Map(static value => value.GetEnum<MapKind>()),
            Floors = floors.Map(static values => values.GetList(static value => value.GetInt32())),
            RegionId = regionId.Map(static value => value.GetInt32()),
            RegionName = regionName.Map(static value => value.GetString()) ?? "",
            ContinentId = continentId.Map(static value => value.GetInt32()),
            ContinentName = continentName.Map(static value => value.GetString()) ?? "",
            MapRectangle = mapRectangle.Map(static value => value.GetMapRectangle()),
            ContinentRectangle =
                continentRectangle.Map(static value => value.GetContinentRectangle())
        };
    }
}
