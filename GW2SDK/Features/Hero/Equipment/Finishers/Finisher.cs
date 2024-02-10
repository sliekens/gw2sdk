namespace GuildWars2.Hero.Equipment.Finishers;

/// <summary>Information about a finisher.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Finisher
{
    /// <summary>The finisher ID.</summary>
    public required int Id { get; init; }

    /// <summary>A description of how to obtain the finisher, as it appears in the tooltip of a locked finisher.</summary>
    public required string LockedText { get; init; }

    /// <summary>The IDs of the items that unlock the finisher when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the finisher in the equipment panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the finisher icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The name of the finisher.</summary>
    public required string Name { get; init; }
}
