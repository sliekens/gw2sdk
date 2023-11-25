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
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == nameContested.Name)
            {
                nameContested = member;
            }
            else if (member.Name == pointOfInterestId.Name)
            {
                pointOfInterestId = member;
            }
            else if (member.Name == coordinates.Name)
            {
                coordinates = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == iconContested.Name)
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
