using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a harpoon gun.</summary>
[JsonConverter(typeof(HarpoonGunJsonConverter))]
public sealed record HarpoonGun : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}
