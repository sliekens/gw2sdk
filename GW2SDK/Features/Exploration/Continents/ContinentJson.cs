using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Continents;

internal static class ContinentJson
{
    public static Continent GetContinent(this JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember continentDimensions = "continent_dims";
        RequiredMember minZoom = "min_zoom";
        RequiredMember maxZoom = "max_zoom";
        RequiredMember floors = "floors";
        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (continentDimensions.Match(member))
            {
                continentDimensions = member;
            }
            else if (minZoom.Match(member))
            {
                minZoom = member;
            }
            else if (maxZoom.Match(member))
            {
                maxZoom = member;
            }
            else if (floors.Match(member))
            {
                floors = member;
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

        return new Continent
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            ContinentDimensions = continentDimensions.Map(static value => value.GetDimensions()),
            MinZoom = minZoom.Map(static value => value.GetInt32()),
            MaxZoom = maxZoom.Map(static value => value.GetInt32()),
            Floors = floors.Map(static values => values.GetList(static value => value.GetInt32()))
        };
    }
}
