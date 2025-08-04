namespace GuildWars2.Hero.Builds;

/// <summary>Information about the selected specialization or elite specialization.</summary>
[DataTransferObject]
public sealed record SelectedSpecialization
{
    /// <summary>The specialization ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the first selected trait for this specialization, or <c>null</c> if no trait was selected.</summary>
    public required int? AdeptTraitId { get; init; }

    /// <summary>The ID of the second selected trait for this specialization, or <c>null</c> if no trait was selected.</summary>
    public required int? MasterTraitId { get; init; }

    /// <summary>The ID of the third selected trait for this specialization, or <c>null</c> if no trait was selected.</summary>
    public required int? GrandmasterTraitId { get; init; }

    /// <summary>Gets the IDs of the selected traits.</summary>
    /// <returns>The IDs of the selected traits.</returns>
    public IEnumerable<int> SelectedTraitIds()
    {
        if (AdeptTraitId.HasValue)
        {
            yield return AdeptTraitId.Value;
        }

        if (MasterTraitId.HasValue)
        {
            yield return MasterTraitId.Value;
        }

        if (GrandmasterTraitId.HasValue)
        {
            yield return GrandmasterTraitId.Value;
        }
    }
}
