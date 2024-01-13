using System.Diagnostics.CodeAnalysis;

namespace GuildWars2.Hero.Builds;

/// <summary>Information about the skills and traits in the build.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    /// <summary>The player-chosen name of this build. Can be empty but not null.</summary>
    public required string Name { get; init; }

    /// <summary>The profession that can use this build.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>The first selected specialization for this build.</summary>
    public required SelectedSpecialization? Specialization { get; init; }

    /// <summary>The second selected specialization for this build.</summary>
    public required SelectedSpecialization? Specialization2 { get; init; }

    /// <summary>The third selected specialization for this build.</summary>
    public required SelectedSpecialization? Specialization3 { get; init; }

    /// <summary>The skills selected for this build.</summary>
    public required SkillBar Skills { get; init; }

    /// <summary>The underwater skills selected for this build.</summary>
    public required SkillBar AquaticSkills { get; init; }

    /// <summary>The pet skills selected for this build. (Rangers only.)</summary>
    public required PetSkillBar? PetSkills { get; init; }

    /// <summary>The legends selected for this build. (Revenants only.)</summary>
    public required SelectedLegends? Legends { get; init; }

    /// <summary>Indicates whether <see cref="PetSkills" /> are present.</summary>
    [MemberNotNullWhen(true, nameof(PetSkills))]
    public bool IsRangerBuild => Profession == ProfessionName.Ranger;

    /// <summary>Indicates whether <see cref="Legends" /> are present.</summary>
    [MemberNotNullWhen(true, nameof(Legends))]
    public bool IsRevenantBuild => Profession == ProfessionName.Revenant;
}
