using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The weight classes of armor.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(WeightClassJsonConverter))]
public enum WeightClass
{
    /// <summary>No specific weight class or unknown weight class.</summary>
    None,

    /// <summary>Clothing can be worn by any profession.</summary>
    Clothing,

    /// <summary>Light armor can be worn by the <see cref="ProfessionName.Elementalist" />,
    /// <see cref="ProfessionName.Mesmer" />, and <see cref="ProfessionName.Necromancer" /> professions.</summary>
    Light,

    /// <summary>Medium armor can be worn by the <see cref="ProfessionName.Engineer" />, <see cref="ProfessionName.Ranger" />,
    /// and <see cref="ProfessionName.Thief" /> professions.</summary>
    Medium,

    /// <summary>Heavy armor can be worn by the <see cref="ProfessionName.Guardian" />, <see cref="ProfessionName.Revenant" />,
    /// and <see cref="ProfessionName.Warrior" /> professions.</summary>
    Heavy
}
