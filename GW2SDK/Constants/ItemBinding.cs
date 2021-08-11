using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    [DefaultValue(None)]
    public enum ItemBinding
    {
        None,

        Account,

        Character
    }

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
}
