using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Maps.Models;
using JetBrains.Annotations;

namespace GW2SDK.Maps.Json;

[PublicAPI]
public static class RegionReader
{
    public static WorldRegion Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<PointF> labelCoordinates = new("label_coord");
        RequiredMember<ContinentRectangle> continentRectangle = new("continent_rect");
        RequiredMember<Dictionary<int, Map>> maps = new("maps");
        RequiredMember<int> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates = labelCoordinates.From(member.Value);
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle = continentRectangle.From(member.Value);
            }
            else if (member.NameEquals(maps.Name))
            {
                maps = maps.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WorldRegion
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            LabelCoordinates = labelCoordinates.Select(value => ReadPointF(value, missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Select(value => ReadContinentRectangle(value, missingMemberBehavior)),
            Maps = maps.Select(value => ReadMaps(value, missingMemberBehavior))
        };
    }

    private static PointF ReadPointF(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        var x = json[0]
            .GetSingle();
        var y = json[1]
            .GetSingle();
        return new PointF(x, y);
    }

    private static ContinentRectangle ReadContinentRectangle(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var topLeft = json[0];
        var x = topLeft[0]
            .GetSingle();
        var y = topLeft[1]
            .GetSingle();
        var size = json[1];
        var width = size[0]
            .GetSingle();
        var height = size[1]
            .GetSingle();
        return new ContinentRectangle
        {
            TopLeft = new PointF(x, y),
            Size = new SizeF(width, height)
        };
    }

    private static Dictionary<int, Map> ReadMaps(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        Dictionary<int, Map> maps = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                maps[id] = MapReader.Read(member.Value, missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return maps;
    }
}
