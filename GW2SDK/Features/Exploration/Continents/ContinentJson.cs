using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Continents;

[PublicAPI]
public static class ContinentJson
{
    public static Continent GetContinent(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember continentDimensions = new("continent_dims");
        RequiredMember minZoom = new("min_zoom");
        RequiredMember maxZoom = new("max_zoom");
        RequiredMember floors = new("floors");
        RequiredMember id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(continentDimensions.Name))
            {
                continentDimensions.Value = member.Value;
            }
            else if (member.NameEquals(minZoom.Name))
            {
                minZoom.Value = member.Value;
            }
            else if (member.NameEquals(maxZoom.Name))
            {
                maxZoom.Value = member.Value;
            }
            else if (member.NameEquals(floors.Name))
            {
                floors.Value = member.Value;
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

        return new Continent
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            ContinentDimensions =
                continentDimensions.Select(value => value.GetDimensions(missingMemberBehavior)),
            MinZoom = minZoom.Select(value => value.GetInt32()),
            MaxZoom = maxZoom.Select(value => value.GetInt32()),
            Floors = floors.SelectMany(value => value.GetInt32())
        };
    }
}
