using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class ScoreJson
{
    public static Score GetScore(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember score = new("value");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(score.Name))
            {
                score.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Score
        {
            Id = id.Select(value => value.GetStringRequired()),
            Value = score.Select(value => value.GetInt32())
        };
    }
}
