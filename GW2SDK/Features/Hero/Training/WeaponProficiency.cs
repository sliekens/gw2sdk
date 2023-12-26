namespace GuildWars2.Hero.Training;

/// <summary>Information about a weapon that can be trained.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record WeaponProficiency
{
    /// <summary>The ID of the elite specialization that is required to train the weapon.</summary>
    public required int? RequiredSpecialization { get; init; }

    /// <summary>Contains various modifiers for the weapon.</summary>
    public required WeaponFlags Flags { get; init; }

    /// <summary>The skills that occupy the weapon skill slots when the weapon is equipped.</summary>
    public required IReadOnlyList<WeaponSkill> Skills { get; init; }
}
