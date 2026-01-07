using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>The equipment slots in which an item can be equipped.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(EquipmentSlotJsonConverter))]
public enum EquipmentSlot
{
    /// <summary>No specific equipment slot or unknown equipment slot.</summary>
    None,

    /// <summary>The first accessory slot.</summary>
    Accessory1,

    /// <summary>The second accessory slot.</summary>
    Accessory2,

    /// <summary>The amulet slot.</summary>
    Amulet,

    /// <summary>The logging axe slot.</summary>
    Axe,

    /// <summary>The back item slot.</summary>
    Back,

    /// <summary>The boots slot.</summary>
    Boots,

    /// <summary>The coat slot.</summary>
    Coat,

    /// <summary>The fishing bait slot.</summary>
    FishingBait,

    /// <summary>The fishing lure slot.</summary>
    FishingLure,

    /// <summary>The fishing rod slot.</summary>
    FishingRod,

    /// <summary>The gloves slot.</summary>
    Gloves,

    /// <summary>The headgear slot.</summary>
    Helm,

    /// <summary>The aquatic headgear slot.</summary>
    HelmAquatic,

    /// <summary>The leggings slot.</summary>
    Leggings,

    /// <summary>The mining pick slot.</summary>
    Pick,

    /// <summary>The Jade bot power core slot</summary>
    PowerCore,

    /// <summary>The relic slot.</summary>
    Relic,

    /// <summary>The first ring slot.</summary>
    Ring1,

    /// <summary>The second ring slot.</summary>
    Ring2,

    /// <summary>The Jade bot sensory array slot.</summary>
    SensoryArray,

    /// <summary>The Jade bot service chip slot.</summary>
    ServiceChip,

    /// <summary>The shoulders slot.</summary>
    Shoulders,

    /// <summary>The foraging sickle slot.</summary>
    Sickle,

    /// <summary>The primary main hand slot.</summary>
    WeaponA1,

    /// <summary>The primary off-hand slot.</summary>
    WeaponA2,

    /// <summary>The primary aquatic weapon slot.</summary>
    WeaponAquaticA,

    /// <summary>The secondary aquatic weapon slot.</summary>
    WeaponAquaticB,

    /// <summary>The secondary main hand slot.</summary>
    WeaponB1,

    /// <summary>The secondary off-hand slot.</summary>
    WeaponB2
}
