using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Continents;

internal static class ContinentJson
{
    public static Continent GetContinent(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember continentDimensions = "continent_dims";
        RequiredMember minZoom = "min_zoom";
        RequiredMember maxZoom = "max_zoom";
        RequiredMember floors = "floors";
        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(continentDimensions.Name))
            {
                continentDimensions = member;
            }
            else if (member.NameEquals(minZoom.Name))
            {
                minZoom = member;
            }
            else if (member.NameEquals(maxZoom.Name))
            {
                maxZoom = member;
            }
            else if (member.NameEquals(floors.Name))
            {
                floors = member;
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

        return new Continent
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            ContinentDimensions =
                continentDimensions.Map(value => value.GetDimensions(missingMemberBehavior)),
            MinZoom = minZoom.Map(value => value.GetInt32()),
            MaxZoom = maxZoom.Map(value => value.GetInt32()),
            Floors = floors.Map(values => values.GetList(value => value.GetInt32()))
        };
    }
}
