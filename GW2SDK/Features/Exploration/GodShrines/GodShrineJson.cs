using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.GodShrines;

[PublicAPI]
public static class GodShrineJson
{
    public static GodShrine GetGodShrine(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember nameContested = "name_contested";
        RequiredMember pointOfInterestId = "poi_id";
        RequiredMember coordinates = "coord";
        RequiredMember icon = "icon";
        RequiredMember iconContested = "icon_contested";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(nameContested.Name))
            {
                nameContested = member;
            }
            else if (member.NameEquals(pointOfInterestId.Name))
            {
                pointOfInterestId = member;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(iconContested.Name))
            {
                iconContested = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GodShrine
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            NameContested = nameContested.Select(value => value.GetStringRequired()),
            PointOfInterestId = pointOfInterestId.Select(value => value.GetInt32()),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            Icon = icon.Select(value => value.GetStringRequired()),
            IconContested = iconContested.Select(value => value.GetStringRequired())
        };
    }
}
