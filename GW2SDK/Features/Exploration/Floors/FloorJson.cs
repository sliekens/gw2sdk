using System.Text.Json;
using GuildWars2.Exploration.Regions;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Floors;

internal static class FloorJson
{
    public static Floor GetFloor(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember textureDimensions = "texture_dims";
        OptionalMember clampedView = "clamped_view";
        RequiredMember regions = "regions";

        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == textureDimensions.Name)
            {
                textureDimensions = member;
            }
            else if (member.Name == clampedView.Name)
            {
                clampedView = member;
            }
            else if (member.Name == regions.Name)
            {
                regions = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Floor
        {
            Id = id.Map(value => value.GetInt32()),
            TextureDimensions =
                textureDimensions.Map(value => value.GetDimensions(missingMemberBehavior)),
            ClampedView =
                clampedView.Map(value => value.GetContinentRectangle(missingMemberBehavior)),
            Regions = regions.Map(
                value => value.GetMap(entry => entry.GetRegion(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            )
        };
    }
}
