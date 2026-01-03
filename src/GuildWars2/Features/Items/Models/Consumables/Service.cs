using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable which immediately takes effect upon receipt.</summary>
[JsonConverter(typeof(ServiceJsonConverter))]
public sealed record Service : Consumable
{
    /// <summary>The effect applied upon receipt.</summary>
    public required Effect? Effect { get; init; }

    /// <summary>The ID of the guild upgrade that is unlocked upon receipt.</summary>
    public required int? GuildUpgradeId { get; init; }
}
