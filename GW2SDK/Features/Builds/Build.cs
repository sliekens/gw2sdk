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
    public required IReadOnlyCollection<string?>? Legends { get; init; }

    /// <summary>The underwater legends selected for this build. (Revenants only.)</summary>
    public required IReadOnlyCollection<string?>? AquaticLegends { get; init; }
}
