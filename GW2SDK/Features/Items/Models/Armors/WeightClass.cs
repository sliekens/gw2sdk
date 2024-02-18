using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>The weight classes of armor.</summary>
[PublicAPI]
public enum WeightClass
{
    /// <summary>Clothing can be worn by any profession.</summary>
    Clothing = 1,

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
