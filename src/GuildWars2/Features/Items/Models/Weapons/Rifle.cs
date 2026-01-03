using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a rifle.</summary>
[JsonConverter(typeof(RifleJsonConverter))]
public sealed record Rifle : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}
