using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

[PublicAPI]
public static class PvpRatingTypeJson
{
    public static PvpRatingType GetRatingType(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        var text = json.GetStringRequired();
        return text switch
        {
            "None" => PvpRatingType.None,
            "Ranked" => PvpRatingType.Ranked,
            "2v2Ranked" => PvpRatingType.Ranked2v2,
            "3v3Ranked" => PvpRatingType.Ranked3v3,
            "Unranked" => PvpRatingType.Unranked,
            "Placeholder" => PvpRatingType.Placeholder,
            _ when missingMemberBehavior is MissingMemberBehavior.Error =>
                throw new InvalidOperationException(Strings.UnexpectedMember(text)),
            _ => (PvpRatingType)text.GetDeterministicHashCode()
        };
    }
}
