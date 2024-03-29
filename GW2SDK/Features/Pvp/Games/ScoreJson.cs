using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

internal static class ScoreJson
{
    public static Score GetScore(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Score
        {
            Red = red.Map(value => value.GetInt32()),
            Blue = blue.Map(value => value.GetInt32())
        };
    }
}
