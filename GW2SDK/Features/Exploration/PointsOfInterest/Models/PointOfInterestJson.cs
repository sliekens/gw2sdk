using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

[PublicAPI]
public static class PointOfInterestJson
{
    public static PointOfInterest GetPointOfInterest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "landmark":
                return json.GetLandmark(missingMemberBehavior);
            case "waypoint":
                return json.GetWaypoint(missingMemberBehavior);
            case "vista":
                return json.GetVista(missingMemberBehavior);
            case "unlock":
                return json.GetUnlockerPointOfInterest(missingMemberBehavior);
        }

        OptionalMember name = new("name");
        RequiredMember floor = new("floor");
        RequiredMember coordinates = new("coord");
        RequiredMember id = new("id");
        RequiredMember chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(floor.Name))
            {
                floor.Value = member.Value;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PointOfInterest
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetString()) ?? "",
            Floor = floor.Select(value => value.GetInt32()),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            ChatLink = chatLink.Select(value => value.GetStringRequired())
        };
    }
}
