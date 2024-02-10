namespace GuildWars2.Hero.Equipment.Finishers;

[PublicAPI]
[DataTransferObject]
public sealed record Finisher
{
    public required int Id { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string UnlockDetails { get; init; }

    /// <summary>The IDs of the items that unlock the finisher when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the finisher in the equipment panel.</summary>
    public required int Order { get; init; }

    public required string IconHref { get; init; }

    public required string Name { get; init; }
}
