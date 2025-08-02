using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

internal static class ScoreJson
{
    public static Score GetScore(this in JsonElement json)
    {
        RequiredMember red = "red";
        RequiredMember blue = "blue";

        foreach (var member in json.EnumerateObject())
        {
            if (red.Match(member))
            {
                red = member;
            }
            else if (blue.Match(member))
            {
                blue = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Score
        {
            Red = red.Map(static (in JsonElement value) => value.GetInt32()),
            Blue = blue.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
