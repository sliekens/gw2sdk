using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Hearts;

[PublicAPI]
public static class HeartJson
{
    public static Heart GetHeart(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember objective = "objective";
        RequiredMember level = "level";
        RequiredMember coordinates = "coord";
        RequiredMember boundaries = "bounds";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(objective.Name))
            {
                objective = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates = member;
            }
            else if (member.NameEquals(boundaries.Name))
            {
                boundaries = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Heart
        {
            Id = id.Select(value => value.GetInt32()),
            Objective = objective.Select(value => value.GetStringRequired()),
            Level = level.Select(value => value.GetInt32()),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            Boundaries = boundaries.SelectMany(value => value.GetCoordinateF(missingMemberBehavior)),
            ChatLink = chatLink.Select(value => value.GetStringRequired())
        };
    }
}
