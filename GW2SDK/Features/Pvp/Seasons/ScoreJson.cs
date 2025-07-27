using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class ScoreJson
{
    public static Score GetScore(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember score = "value";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (score.Match(member))
            {
                score = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Score
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Value = score.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
