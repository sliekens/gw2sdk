using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Wvw.Matches;

/// <summary>The bonus kinds.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(BonusKindJsonConverter))]
public enum BonusKind
{
    /// <summary>No specific bonus or unknown bonus.</summary>
    None,

    /// <summary>Borderlands Bloodlust, gained by holding three ruins.</summary>
    Bloodlust
}
