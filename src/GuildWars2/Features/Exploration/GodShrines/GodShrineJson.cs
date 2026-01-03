using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration.GodShrines;

internal static class GodShrineJson
{
    public static GodShrine GetGodShrine(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember nameContested = "name_contested";
        RequiredMember pointOfInterestId = "poi_id";
        RequiredMember coordinates = "coord";
        RequiredMember icon = "icon";
        RequiredMember iconContested = "icon_contested";
        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GodShrine
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            NameContested = nameContested.Map(static (in value) => value.GetStringRequired()),
            PointOfInterestId = pointOfInterestId.Map(static (in value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in value) => value.GetCoordinateF()),
#pragma warning disable CS0618 // Suppress obsolete warning
            IconHref = icon.Map(static (in value) => value.GetStringRequired()),
            IconContestedHref = iconContested.Map(static (in value) => value.GetStringRequired()),
#pragma warning restore CS0618
            IconUrl = icon.Map(static (in value) => new Uri(value.GetStringRequired())),
            IconContestedUrl = iconContested.Map(static (in value) => new Uri(value.GetStringRequired()))
        };
    }
}
