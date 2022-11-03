using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Exploration.Charts;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Regions;

[PublicAPI]
public static class RegionReader
{
    public static Region GetRegion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<PointF> labelCoordinates = new("label_coord");
        RequiredMember<Area> continentRectangle = new("continent_rect");
        RequiredMember<Dictionary<int, Chart>> maps = new("maps");
        RequiredMember<int> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates.Value = member.Value;
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle.Value = member.Value;
            }
            else if (member.NameEquals(maps.Name))
            {
                maps.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Region
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            LabelCoordinates =
                labelCoordinates.Select(value => value.GetCoordinate(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => value.GetArea(missingMemberBehavior)),
            Maps = maps.Select(value => value.GetRegionCharts(missingMemberBehavior))
        };
    }
}
