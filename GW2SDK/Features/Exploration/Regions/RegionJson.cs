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
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            LabelCoordinates =
                labelCoordinates.Map(value => value.GetCoordinate(missingMemberBehavior)),
            ContinentRectangle =
                continentRectangle.Map(value => value.GetContinentRectangle(missingMemberBehavior)),
            Maps = maps.Map(
                value => value.GetMap(entry => entry.GetMap(missingMemberBehavior))
                    .ToDictionary(kvp => int.Parse(kvp.Key), kvp => kvp.Value)
            )
        };
    }
}
