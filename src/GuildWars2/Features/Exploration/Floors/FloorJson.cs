using System.Globalization;
using System.Text.Json;

using GuildWars2.Exploration.Regions;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Floors;

internal static class FloorJson
{
    public static Floor GetFloor(this in JsonElement json)
    {
        RequiredMember textureDimensions = "texture_dims";
        OptionalMember clampedView = "clamped_view";
        RequiredMember regions = "regions";

        RequiredMember id = "id";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (textureDimensions.Match(member))
            {
                textureDimensions = member;
            }
            else if (clampedView.Match(member))
            {
                clampedView = member;
            }
            else if (regions.Match(member))
            {
                regions = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Floor
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            TextureDimensions = textureDimensions.Map(static (in value) => value.GetDimensions()),
            ClampedView = clampedView.Map(static (in value) => value.GetContinentRectangle()),
            Regions = regions.Map(static (in value) => value.GetMap(
                static key => int.Parse(key, CultureInfo.InvariantCulture),
                static (in entry) => entry.GetRegion())
            )
        };
    }
}
