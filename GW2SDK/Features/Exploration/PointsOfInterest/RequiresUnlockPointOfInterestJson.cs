using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal static class RequiresUnlockPointOfInterestJson
{
    public static RequiresUnlockPointOfInterest GetRequiresUnlockPointOfInterest(
        this in JsonElement json
    )
    {
        OptionalMember name = "name";
        RequiredMember floor = "floor";
        RequiredMember coordinates = "coord";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        RequiredMember icon = "icon";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("unlock"))
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
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
#pragma warning disable CS0618
        return new RequiresUnlockPointOfInterest
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Floor = floor.Map(static (in JsonElement value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in JsonElement value) => value.GetCoordinateF()),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired()),
            IconHref = iconString,
            IconUrl = new Uri(iconString)
        };
#pragma warning restore CS0618
    }
}
