using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Hearts;

[PublicAPI]
public static class HeartJson
{
    public static Heart GetHeart(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> objective = new("objective");
        RequiredMember<int> level = new("level");
        RequiredMember<PointF> coordinates = new("coord");
        RequiredMember<PointF> boundaries = new("bounds");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(objective.Name))
            {
                objective.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(coordinates.Name))
            {
                coordinates.Value = member.Value;
            }
            else if (member.NameEquals(boundaries.Name))
            {
                boundaries.Value = member.Value;
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

        return new Heart
        {
            Id = id.GetValue(),
            Objective = objective.GetValue(),
            Level = level.GetValue(),
            Coordinates = coordinates.Select(value => value.GetCoordinateF(missingMemberBehavior)),
            Boundaries = boundaries.SelectMany(value => value.GetCoordinateF(missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }
}
