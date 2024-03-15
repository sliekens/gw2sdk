using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal static class PointOfInterestJson
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
                return json.GetRequiresUnlockPointOfInterest(missingMemberBehavior);
        }

        OptionalMember name = "name";
        RequiredMember floor = "floor";
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (floor.Match(member))
            {
                floor = member;
            }
            else if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PointOfInterest
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetString()) ?? "",
            Floor = floor.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            ChatLink = chatLink.Map(value => value.GetStringRequired())
        };
    }
}
