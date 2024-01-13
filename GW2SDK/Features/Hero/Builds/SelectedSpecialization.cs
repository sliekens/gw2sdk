namespace GuildWars2.Hero.Builds;

/// <summary>Information about the selected specialization or elite specialization.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedSpecialization
{
    /// <summary>The specialization ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the first selected trait for this specialization, or <c>null</c> if no trait was selected.</summary>
    public required int? TraitId1 { get; init; }

    /// <summary>The ID of the second selected trait for this specialization, or <c>null</c> if no trait was selected.</summary>
    public required int? TraitId2 { get; init; }

    /// <summary>The ID of the third selected trait for this specialization, or <c>null</c> if no trait was selected.</summary>
    public required int? TraitId3 { get; init; }

    /// <summary>Gets the IDs of the selected traits.</summary>
    /// <returns>The IDs of the selected traits.</returns>
    public IEnumerable<int> SelectedTraitIds()
    {
        if (TraitId1.HasValue)
        {
            yield return TraitId1.Value;
        }

        if (TraitId2.HasValue)
        {
            yield return TraitId2.Value;
        }

        if (TraitId3.HasValue)
        {
            yield return TraitId3.Value;
        }
    }
}
