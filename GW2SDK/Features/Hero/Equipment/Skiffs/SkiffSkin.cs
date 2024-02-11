namespace GuildWars2.Hero.Equipment.Skiffs;

/// <summary>Information about a skiff skin.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkiffSkin
{
    /// <summary>The skiff skin ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the skiff skin.</summary>
    public required string Name { get; init; }

    /// <summary>The URL of the skiff skin icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The dyes applied by default.</summary>
    public required IReadOnlyList<DyeSlot> DyeSlots { get; init; }
}
