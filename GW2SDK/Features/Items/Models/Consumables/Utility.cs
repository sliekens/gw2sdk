using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a utility consumable, which typically grants an enhancement effect when consumed.</summary>
[PublicAPI]
[JsonConverter(typeof(UtilityJsonConverter))]
public sealed record Utility : Consumable
{
    /// <summary>The effect applied when the item is consumed.</summary>
    public required Effect? Effect { get; init; }
}
