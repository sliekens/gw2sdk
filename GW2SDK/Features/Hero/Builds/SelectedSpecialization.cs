namespace GuildWars2.Hero.Builds;

/// <summary>Information about the selected specialization or elite specialization.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedSpecialization
{
    /// <summary>The specialization ID or <c>null</c> if no specialization was selected.</summary>
    public required int? Id { get; init; }

    /// <summary>The IDs of the selected traits. This list is always length 3 because there are 3 trait slots per
    /// specialization. Empty trait slots are represented as <c>null</c>.</summary>
    public required IReadOnlyList<int?> TraitIds { get; init; }
}
