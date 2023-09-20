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
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<string> nameContested = new("name_contested");
        RequiredMember<int> pointOfInterestId = new("poi_id");
        RequiredMember<double> coordinates = new("coord");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> iconContested = new("icon_contested");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(nameContested.Name))
            {
                nameContested.Value = member.Value;
            }
            else if (member.NameEquals(pointOfInterestId.Name))
            {
                pointOfInterestId.Value = member.Value;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(iconContested.Name))
            {
                iconContested.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GodShrine
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            NameContested = nameContested.GetValue(),
            PointOfInterestId = pointOfInterestId.GetValue(),
            Coordinates = coordinates.SelectMany(value => value.GetDouble()),
            Icon = icon.GetValue(),
            IconContested = iconContested.GetValue()
        };
    }
}
