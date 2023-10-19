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
        RequiredMember name = "name";
        RequiredMember labelCoordinates = "label_coord";
        RequiredMember continentRectangle = "continent_rect";
        RequiredMember maps = "maps";
        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(labelCoordinates.Name))
            {
                labelCoordinates = member;
            }
            else if (member.NameEquals(continentRectangle.Name))
            {
                continentRectangle = member;
            }
            else if (member.NameEquals(maps.Name))
            {
                maps = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Region
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
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
