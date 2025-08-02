using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>The types of infusion slot upgrades.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(InfusionSlotUpgradeKindJsonConverter))]
public enum InfusionSlotUpgradeKind
{
    /// <summary>No specific upgrade or unknown upgrade.</summary>
    None,

    /// <summary>Attunement is a process where an ascended ring is upgraded in the Mystic Forge to gain one extra infusion
    /// slot.</summary>
    Attunement,

    /// <summary>Infusion is a process where an ascended ring or back item is upgraded in the Mystic Forge to gain one extra
    /// infusion.</summary>
    Infusion
}
