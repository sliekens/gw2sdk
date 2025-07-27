using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Hearts;

internal static class HeartJson
{
    public static Heart GetHeart(this in JsonElement json)
    {
        RequiredMember objective = "objective";
        RequiredMember level = "level";
        RequiredMember coordinates = "coord";
        RequiredMember boundaries = "bounds";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (objective.Match(member))
            {
                objective = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (coordinates.Match(member))
            {
                coordinates = member;
            }
            else if (boundaries.Match(member))
            {
                boundaries = member;
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

        return new Heart
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Objective = objective.Map(static (in JsonElement value) => value.GetStringRequired()),
            Level = level.Map(static (in JsonElement value) => value.GetInt32()),
            Coordinates = coordinates.Map(static (in JsonElement value) => value.GetCoordinateF()),
            Boundaries =
                boundaries.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetCoordinateF())
                ),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
