using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a trident.</summary>
[PublicAPI]
[JsonConverter(typeof(TridentJsonConverter))]
public sealed record Trident : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}
