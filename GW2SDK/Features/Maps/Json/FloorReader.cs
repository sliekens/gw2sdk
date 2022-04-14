using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;

using GW2SDK.Json;
using GW2SDK.Maps.Models;

using JetBrains.Annotations;

namespace GW2SDK.Maps.Json;

[PublicAPI]
public static class FloorReader
{
    public static Floor Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<SizeF> textureDimensions = new("texture_dims");
        OptionalMember<ContinentRectangle> clampedView = new("clamped_view");
        RequiredMember<Dictionary<int, WorldRegion>> regions = new("regions");
        RequiredMember<int> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(textureDimensions.Name))
            {
                textureDimensions = textureDimensions.From(member.Value);
            }
            else if (member.NameEquals(clampedView.Name))
            {
                clampedView = clampedView.From(member.Value);
            }
            else if (member.NameEquals(regions.Name))
            {
                regions = regions.From(member.Value);
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

        return new Floor
        {
            Id = id.GetValue(),
            TextureDimensions = textureDimensions.Select(value => ReadSizeF(value, missingMemberBehavior)),
            ClampedView = clampedView.Select(value => ReadContinentRectangle(value, missingMemberBehavior)),
            Regions = regions.Select(value => ReadRegions(value, missingMemberBehavior))
        };
    }

    private static SizeF ReadSizeF(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        var width = json[0]
            .GetSingle();
        var height = json[1]
            .GetSingle();
        return new SizeF(width, height);
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

    private static Dictionary<int, WorldRegion> ReadRegions(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        Dictionary<int, WorldRegion> regions = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                regions[id] = RegionReader.Read(member.Value, missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return regions;
    }
}