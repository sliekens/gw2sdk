using System.Drawing;
using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Regions;

[PublicAPI]
public static class RegionJson
{
    public static Region GetRegion(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<Point> labelCoordinates = new("label_coord");
        RequiredMember<Rectangle> continentRectangle = new("continent_rect");
        RequiredMember<Dictionary<int, Map>> maps = new("maps");
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
                continentRectangle.Select(value => value.GetContinentRectangle(missingMemberBehavior)),
            Maps = maps.Select(
                value => value.GetMap(entry => entry.GetMap(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            ),
        };
    }
}
