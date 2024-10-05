namespace GuildWars2.Pve.Home.Decorations;

/// <summary>Information about a homestead decoration.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Decoration
{
    /// <summary>The decoration ID.</summary>
    public required int Id { get; init; }

    /// <summary>The decoration name.</summary>
    public required string Name { get; init; }

    /// <summary>The decoration description.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <summary>The category IDs associated with the decoration.</summary>
    public required IReadOnlyList<int> CategoryIds { get; init; }

    /// <summary>The maximum number of this decoration that can be owned.</summary>
    public required int MaxCount { get; init; }

    /// <summary>The decoration icon URL.</summary>
    public required string IconHref { get; init; }
}
