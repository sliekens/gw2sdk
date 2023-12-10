namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>The location of an equipment item.</summary>
[PublicAPI]
public enum EquipmentLocation
{
    /// <summary>The item is equipped in the current active template.</summary>
    Equipped = 1,

    /// <summary>The item is equipped in an inactive template.</summary>
    Armory,

    /// <summary>The item is equipped in the current active template, and it is Legendary.</summary>
    EquippedFromLegendaryArmory,

    /// <summary>The item is equipped in an inactive template, and it is Legendary.</summary>
    LegendaryArmory
}
