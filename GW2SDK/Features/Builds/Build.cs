using System.Diagnostics.CodeAnalysis;

namespace GuildWars2.Builds;

/// <summary>Information about the skills and traits in the build.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    /// <summary>The player-chosen name of this build. Can be empty but not null.</summary>
    public required string Name { get; init; }

    /// <summary>The profession that can use this build.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>The specializations selected for this build.</summary>
    public required IReadOnlyList<SelectedSpecialization> Specializations { get; init; }

    /// <summary>The skills selected for this build.</summary>
    public required SkillBar Skills { get; init; }

    /// <summary>The underwater skills selected for this build.</summary>
    public required SkillBar AquaticSkills { get; init; }

    /// <summary>The pet skills selected for this build. (Rangers only.)</summary>
    public required PetSkillBar? PetSkills { get; init; }

    /// <summary>The legends selected for this build. (Revenants only.)</summary>
    public required IReadOnlyList<string?>? Legends { get; init; }

    /// <summary>The underwater legends selected for this build. (Revenants only.)</summary>
    public required IReadOnlyList<string?>? AquaticLegends { get; init; }

    /// <summary>Indicates whether <see cref="PetSkills" /> are present.</summary>
    [MemberNotNullWhen(true, nameof(PetSkills))]
    public bool IsRangerBuild => Profession == ProfessionName.Ranger;

    /// <summary>Indicates whether <see cref="Legends" /> and <see cref="AquaticLegends" /> are present.</summary>
    [MemberNotNullWhen(true, nameof(Legends), nameof(AquaticLegends))]
    public bool IsRevenantBuild => Profession == ProfessionName.Revenant;
}
