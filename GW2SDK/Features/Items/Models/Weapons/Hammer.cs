using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a hammer.</summary>
[PublicAPI]
[JsonConverter(typeof(HammerJsonConverter))]
public sealed record Hammer : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}
