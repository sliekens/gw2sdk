namespace GuildWars2.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record Baggage
{
    public required IReadOnlyCollection<Bag?> Bags { get; init; }
}
