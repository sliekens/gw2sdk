using System.Text.Json;
using GuildWars2.Exploration.Maps;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Regions;

internal static class RegionJson
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
            if (name.Match(member))
            {
                name = member;
            }
            else if (labelCoordinates.Match(member))
            {
                labelCoordinates = member;
            }
            else if (continentRectangle.Match(member))
            {
                continentRectangle = member;
            }
            else if (maps.Match(member))
            {
                maps = member;
            }
            else if (id.Match(member))
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
