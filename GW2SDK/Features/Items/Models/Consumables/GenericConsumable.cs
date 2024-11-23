using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a generic consumable.</summary>
[PublicAPI]
[JsonConverter(typeof(GenericConsumableJsonConverter))]
public sealed record GenericConsumable : Consumable
{
    /// <summary>The effect applied when the item is consumed.</summary>
    public required Effect? Effect { get; init; }

    /// <summary>The ID of the guild upgrade that is unlocked when the item is consumed.</summary>
    public required int? GuildUpgradeId { get; init; }
}
