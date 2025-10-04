using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal static class WaypointJson
{
    public static Waypoint GetWaypoint(this in JsonElement json)
    {
        OptionalMember name = "name";
        RequiredMember floor = "floor";
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("waypoint"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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

        return new Waypoint
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetString()) ?? "",
            Floor = floor.Map(static (in value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in value) => value.GetCoordinateF()),
            ChatLink = chatLink.Map(static (in value) => value.GetStringRequired())
        };
    }
}
