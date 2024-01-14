namespace GuildWars2.Hero.Builds;

/// <summary>Information about the selected legends for a Revenant build.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedLegends
{
    /// <summary>The ID of the first selected legend, or <c>null</c> if no legend was selected.</summary>
    public required string? Terrestrial1 { get; init; }

    /// <summary>The ID of the other selected legend, or <c>null</c> if no legend was selected.</summary>
    public required string? Terrestrial2 { get; init; }

    /// <summary>The ID of the first selected legend for underwater, or <c>null</c> if no legend was selected.</summary>
    public required string? Aquatic1 { get; init; }

    /// <summary>The ID of the other selected legend for underwater, or <c>null</c> if no legend was selected.</summary>
    public required string? Aquatic2 { get; init; }
}
