using System.Text.Json.Serialization;

namespace GuildWars2.Wvw.Matches;

/// <summary>The bonus kinds.</summary>
[PublicAPI]
[JsonConverter(typeof(BonusKindJsonConverter))]
public enum BonusKind
{
    /// <summary>Borderlands Bloodlust, gained by holding three ruins.</summary>
    Bloodlust = 1
}
