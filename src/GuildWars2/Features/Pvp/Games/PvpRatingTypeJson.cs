using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Games;

internal static class PvpRatingTypeJson
{
    public static Extensible<PvpRatingType> GetRatingType(this in JsonElement json)
    {
        string text = json.GetStringRequired();
        return text switch
        {
            "None" => PvpRatingType.None,
            "Ranked" => PvpRatingType.Ranked,
            "2v2Ranked" => PvpRatingType.Ranked2v2,
            "3v3Ranked" => PvpRatingType.Ranked3v3,
            "CtFRanked" => PvpRatingType.RankedPush,
            "Unranked" => PvpRatingType.Unranked,
            "Placeholder" => PvpRatingType.Placeholder,
            _ => new Extensible<PvpRatingType>(text)
        };
    }
}
