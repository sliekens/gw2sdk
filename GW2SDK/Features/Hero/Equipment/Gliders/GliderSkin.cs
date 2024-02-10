namespace GuildWars2.Hero.Equipment.Gliders;

/// <summary>Information about a glider skin.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GliderSkin
{
    public required int Id { get; init; }

    /// <summary>The IDs of the items that unlock the glider skin when consumed.</summary>
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
