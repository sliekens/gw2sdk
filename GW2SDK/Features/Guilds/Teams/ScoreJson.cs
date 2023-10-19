using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Teams;

[PublicAPI]
public static class ScoreJson
{
    public static Score GetScore(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember red = "red";
        RequiredMember blue = "blue";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(red.Name))
            {
                red = member;
            }
            else if (member.NameEquals(blue.Name))
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
