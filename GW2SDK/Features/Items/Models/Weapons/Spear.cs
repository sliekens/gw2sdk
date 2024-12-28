using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a spear.</summary>
[PublicAPI]
[JsonConverter(typeof(SpearJsonConverter))]
public sealed record Spear : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}
