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
    public required Extensible<ProfessionName> Profession { get; init; }

    /// <summary>The first selected specialization, or <c>null</c> if no specialization was selected.</summary>
    public required SelectedSpecialization? Specialization1 { get; init; }

    /// <summary>The second selected specialization, or <c>null</c> if no specialization was selected.</summary>
    public required SelectedSpecialization? Specialization2 { get; init; }

    /// <summary>The third selected specialization, or <c>null</c> if no specialization was selected.</summary>
    public required SelectedSpecialization? Specialization3 { get; init; }

    /// <summary>The selected skills.</summary>
    public required SkillBar Skills { get; init; }

    /// <summary>The selected skills for underwater.</summary>
    public required SkillBar AquaticSkills { get; init; }

    /// <summary>The selected pets for Rangers, or <c>null</c> for other professions.</summary>
    public required SelectedPets? Pets { get; init; }

    /// <summary>The selected legends for Revenants, or <c>null</c> for other professions.</summary>
    public required SelectedLegends? Legends { get; init; }

    /// <summary>Indicates whether <see cref="Pets" /> are present.</summary>
    [MemberNotNullWhen(true, nameof(Pets))]
    public bool IsRangerBuild => Profession == ProfessionName.Ranger;

    /// <summary>Indicates whether <see cref="Legends" /> are present.</summary>
    [MemberNotNullWhen(true, nameof(Legends))]
    public bool IsRevenantBuild => Profession == ProfessionName.Revenant;

    /// <summary>Gets the IDs of the selected specializations.</summary>
    /// <returns>The IDs of the selected specializations.</returns>
    public IEnumerable<int> SelectedSpecializationIds()
    {
        if (Specialization1 is not null)
        {
            yield return Specialization1.Id;
        }

        if (Specialization2 is not null)
        {
            yield return Specialization2.Id;
        }

        if (Specialization3 is not null)
        {
            yield return Specialization3.Id;
        }
    }

    /// <summary>Gets the IDs of the selected traits.</summary>
    /// <returns>The IDs of the selected traits.</returns>
    public IEnumerable<int> SelectedTraitIds()
    {
        foreach (int id in Specialization1?.SelectedTraitIds() ?? [])
        {
            yield return id;
        }

        foreach (int id in Specialization2?.SelectedTraitIds() ?? [])
        {
            yield return id;
        }

        foreach (int id in Specialization3?.SelectedTraitIds() ?? [])
        {
            yield return id;
        }
    }

    /// <summary>Gets the IDs of the selected skills.</summary>
    /// <returns>The IDs of the selected skills.</returns>
    public IEnumerable<int> SelectedSkillIds()
    {
        foreach (int id in Skills.SelectedSkillIds())
        {
            yield return id;
        }

        foreach (int id in AquaticSkills.SelectedSkillIds())
        {
            yield return id;
        }
    }
}
