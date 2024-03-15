using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.GodShrines;

internal static class GodShrineJson
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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (nameContested.Match(member))
            {
                nameContested = member;
            }
            else if (pointOfInterestId.Match(member))
            {
                pointOfInterestId = member;
            }
            else if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (iconContested.Match(member))
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
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            NameContested = nameContested.Map(value => value.GetStringRequired()),
            PointOfInterestId = pointOfInterestId.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            IconHref = icon.Map(value => value.GetStringRequired()),
            IconContestedHref = iconContested.Map(value => value.GetStringRequired())
        };
    }
}
