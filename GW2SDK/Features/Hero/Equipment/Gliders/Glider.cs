namespace GuildWars2.Hero.Equipment.Gliders;

[PublicAPI]
[DataTransferObject]
public sealed record Glider
{
    public required int Id { get; init; }

    /// <summary>The IDs of the items that unlock the glider when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the glider in the equipment panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the glider icon.</summary>
    public required string IconHref { get; init; }

    public required string Name { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<int> DefaultDyes { get; init; }
}
