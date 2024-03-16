using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a weapon skin.</summary>
[PublicAPI]
[Inheritable]
public record WeaponSkin : EquipmentSkin
{
    /// <summary>The damage type of the weapon skin.</summary>
    /// <remarks>Damage type does not affect damage. It makes creatures die with a different animation, depending on the damage
    /// type of the final blow.</remarks>
    public required DamageType DamageType { get; init; }
}
