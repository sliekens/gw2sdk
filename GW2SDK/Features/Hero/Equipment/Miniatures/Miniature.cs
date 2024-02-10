namespace GuildWars2.Hero.Equipment.Miniatures;

[PublicAPI]
[DataTransferObject]
public sealed record Miniature
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Unlock { get; init; }

    /// <summary>The URL of the miniature icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The display order of the miniature in the equipment panel.</summary>
    public required int Order { get; init; }

    public required int ItemId { get; init; }
}
