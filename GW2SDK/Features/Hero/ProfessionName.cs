using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The playable professions.</summary>
[PublicAPI]
[JsonConverter(typeof(ProfessionNameJsonConverter))]
public enum ProfessionName
{
    /// <summary>Guardian uses heavy armor and focuses on defensive magic.</summary>
    Guardian = 1,

    /// <summary>Warrior uses heavy armor and focuses on physical attacks.</summary>
    Warrior,

    /// <summary>Engineer uses medium armor and focuses on turrets, gadgets, and elixirs.</summary>
    Engineer,

    /// <summary>Ranger uses medium armor and focuses on ranged attacks and animal companions.</summary>
    Ranger,

    /// <summary>Thief uses medium armor and focuses on stealth, mobility, and stealing from enemies.</summary>
    Thief,

    /// <summary>Elementalist uses light armor and focuses on attunements to switch between different types of magic.</summary>
    Elementalist,

    /// <summary>Mesmer uses light armor and focuses on illusions, shattering them to create powerful effects.</summary>
    Mesmer,

    /// <summary>Necromancer uses light armor and focuses on death magic, draining life from enemies and using it to create
    /// minions.</summary>
    Necromancer,

    /// <summary>Revenant uses heavy armor and focuses on channeling the powers of the Mists through the invocation of legends.</summary>
    Revenant
}
