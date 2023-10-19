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
        RequiredMember red = new("red");
        RequiredMember blue = new("blue");

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
            Red = red.Select(value => value.GetInt32()),
            Blue = blue.Select(value => value.GetInt32())
        };
    }
}
