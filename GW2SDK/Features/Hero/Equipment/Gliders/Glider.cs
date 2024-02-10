namespace GuildWars2.Hero.Equipment.Gliders;

[PublicAPI]
[DataTransferObject]
public sealed record Glider
{
    public required int Id { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<int> UnlockItems { get; init; }

    /// <summary>The display order of the glider in the equipment panel.</summary>
    public required int Order { get; init; }

    public required string IconHref { get; init; }

    public required string Name { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string Description { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<int> DefaultDyes { get; init; }
}
