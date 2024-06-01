using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal static class VistaJson
{
    public static Vista GetVista(this JsonElement json)
    {
        OptionalMember name = "name";
        RequiredMember floor = "floor";
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("vista"))
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

        return new Vista
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetString()) ?? "",
            Floor = floor.Map(static value => value.GetInt32()),
            Coordinates = coordinates.Map(static value => value.GetCoordinateF()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired())
        };
    }
}
