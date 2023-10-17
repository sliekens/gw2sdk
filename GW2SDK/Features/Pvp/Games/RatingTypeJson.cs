using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

[PublicAPI]
public static class RatingTypeJson
{
    public static RatingType GetRatingType(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var text = json.GetStringRequired();
        return text switch
        {
            "None" => RatingType.None,
            "Ranked" => RatingType.Ranked,
            "2v2Ranked" => RatingType.Ranked2v2,
            "3v3Ranked" => RatingType.Ranked3v3,
            "Unranked" => RatingType.Unranked,
            "Placeholder" => RatingType.Placeholder,
            _ when missingMemberBehavior is MissingMemberBehavior.Error =>
                throw new InvalidOperationException(Strings.UnexpectedMember(text)),
            _ => (RatingType)text.GetDeterministicHashCode()
        };
    }
}
