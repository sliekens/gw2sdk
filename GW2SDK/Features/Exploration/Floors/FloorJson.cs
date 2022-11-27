using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;
using Region = GW2SDK.Exploration.Regions.Region;

namespace GW2SDK.Exploration.Floors;

[PublicAPI]
public static class FloorJson
{
    public static Floor GetFloor(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<SizeF> textureDimensions = new("texture_dims");
        OptionalMember<Area> clampedView = new("clamped_view");
        RequiredMember<Dictionary<int, Region>> regions = new("regions");
        RequiredMember<int> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(textureDimensions.Name))
            {
                textureDimensions.Value = member.Value;
            }
            else if (member.NameEquals(clampedView.Name))
            {
                clampedView.Value = member.Value;
            }
            else if (member.NameEquals(regions.Name))
            {
                regions.Value = member.Value;
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

        return new Floor
        {
            Id = id.GetValue(),
            TextureDimensions =
                textureDimensions.Select(value => value.GetDimensions(missingMemberBehavior)),
            ClampedView = clampedView.Select(value => value.GetArea(missingMemberBehavior)),
            Regions = regions.Select(value => value.GetFloorRegions(missingMemberBehavior))
        };
    }
}
