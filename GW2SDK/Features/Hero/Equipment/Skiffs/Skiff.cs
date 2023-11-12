namespace GuildWars2.Hero.Equipment.Skiffs;

/// <summary>Information about a Skiff.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Skiff
{
    /// <summary>The skiff ID.</summary>
    public required int Id { get; init; }

    /// <summary>The skiff name.</summary>
    public required string Name { get; init; }

    /// <summary>The skiff icon URL.</summary>
    public required string IconHref { get; init; }

    /// <summary>The default colors of the skiff.</summary>
    public required IReadOnlyList<DyeSlot> DyeSlots { get; init; }
}
