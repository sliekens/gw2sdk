using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

[PublicAPI]
public static class ScoreJson
{
    public static Score GetScore(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> red = new("red");
        RequiredMember<int> blue = new("blue");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(red.Name))
            {
                red.Value = member.Value;
            }
            else if (member.NameEquals(blue.Name))
            {
                blue.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Score
        {
            Red = red.GetValue(),
            Blue = blue.GetValue()
        };
    }
}
