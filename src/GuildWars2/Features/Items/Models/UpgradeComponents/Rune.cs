using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a rune, which is used to upgrade armor.</summary>
[JsonConverter(typeof(RuneJsonConverter))]
public sealed record Rune : UpgradeComponent
{
    /// <summary>The bonuses provided by this rune.</summary>
    /// <remarks>Each equipped rune gives one bonus from this list. The bonuses are cumulative and depend on the number of
    /// identical runes equipped. For example, if you have two runes of the same type equipped, you will get the first two
    /// bonuses from the list. If you have three of the same runes equipped, you will get the first three bonuses, and so on.</remarks>
    public required IImmutableValueList<string>? Bonuses { get; init; }
}
