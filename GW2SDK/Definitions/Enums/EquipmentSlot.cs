using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public enum EquipmentSlot
{
    Accessory1 = 1,

    Accessory2,

    Amulet,

    Axe,

    Backpack,

    Boots,

    Coat,

    Gloves,

    Helm,

    HelmAquatic,

    Leggings,

    Pick,

    Ring1,

    Ring2,

    Shoulders,

    Sickle,

    /// <summary>Primary main hand.</summary>
    WeaponA1,

    /// <summary>Primary off-hand.</summary>
    WeaponA2,

    /// <summary>Primary aquatic weapon.</summary>
    WeaponAquaticA,

    /// <summary>Secondary aquatic weapon.</summary>
    WeaponAquaticB,

    /// <summary>Secondary main hand.</summary>
    WeaponB1,

    /// <summary>Secondary off-hand.</summary>
    WeaponB2
}
