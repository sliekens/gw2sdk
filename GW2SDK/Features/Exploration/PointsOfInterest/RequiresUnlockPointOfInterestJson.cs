using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.PointsOfInterest;

internal static class RequiresUnlockPointOfInterestJson
{
    public static RequiresUnlockPointOfInterest GetRequiresUnlockPointOfInterest(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
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
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RequiresUnlockPointOfInterest
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetString()) ?? "",
            Floor = floor.Map(value => value.GetInt32()),
            Coordinates = coordinates.Map(value => value.GetCoordinateF(missingMemberBehavior)),
            ChatLink = chatLink.Map(value => value.GetStringRequired()),
            IconHref = icon.Map(value => value.GetStringRequired())
        };
    }
}
