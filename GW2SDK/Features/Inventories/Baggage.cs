namespace GuildWars2.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record Baggage
{
    public required IReadOnlyList<Bag?> Bags { get; init; }
}
