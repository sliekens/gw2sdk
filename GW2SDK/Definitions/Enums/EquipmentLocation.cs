using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public enum EquipmentLocation
{
    /// <summary>Equipped in the active tab.</summary>
    Equipped = 1,

    /// <summary>Equipped in an inactive tab.</summary>
    Armory,

    /// <summary>Equipped in the active tab.</summary>
    EquippedFromLegendaryArmory,

    /// <summary>Equipped in an inactive tab.</summary>
    LegendaryArmory
}
