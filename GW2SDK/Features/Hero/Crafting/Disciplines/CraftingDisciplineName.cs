using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Crafting.Disciplines;

/// <summary>The different crafting disciplines in the game.</summary>
[PublicAPI]
[JsonConverter(typeof(CraftingDisciplineNameJsonConverter))]
public enum CraftingDisciplineName
{
    /// <summary>The Armorsmith crafting discipline.</summary>
    Armorsmith = 1,

    /// <summary>The Artificer crafting discipline.</summary>
    Artificer,

    /// <summary>The Chef crafting discipline.</summary>
    Chef,

    /// <summary>The Huntsman crafting discipline.</summary>
    Huntsman,

    /// <summary>The Jeweler crafting discipline.</summary>
    Jeweler,

    /// <summary>The Leatherworker crafting discipline.</summary>
    Leatherworker,

    /// <summary>The Scribe crafting discipline.</summary>
    Scribe,

    /// <summary>The Tailor crafting discipline.</summary>
    Tailor,

    /// <summary>The Weaponsmith crafting discipline.</summary>
    Weaponsmith,

    /// <summary>The Homesteader crafting discipline.</summary>
    Homesteader
}
