using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal static class PointOfInterestJson
{
    public static PointOfInterest GetPointOfInterest(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "landmark":
                    return json.GetLandmark();
                case "waypoint":
                    return json.GetWaypoint();
                case "vista":
                    return json.GetVista();
                case "unlock":
                    return json.GetRequiresUnlockPointOfInterest();
            }
        }

        OptionalMember name = "name";
        RequiredMember floor = "floor";
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new PointOfInterest
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Floor = floor.Map(static (in JsonElement value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in JsonElement value) => value.GetCoordinateF()),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
