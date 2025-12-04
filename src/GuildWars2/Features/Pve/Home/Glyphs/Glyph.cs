namespace GuildWars2.Pve.Home.Decorations;

/// <summary>Information about a homestead glyph.</summary>
[DataTransferObject]
public sealed record Glyph
{
    /// <summary>The glyph ID.</summary>
    public required string Id { get; init; }

    /// <summary>The item ID of the glyph.</summary>
    public required int ItemId { get; init; }

    /// <summary>The slot where this glyph can be equipped.</summary>
    public required Extensible<CollectionBox> Slot { get; init; }
}
