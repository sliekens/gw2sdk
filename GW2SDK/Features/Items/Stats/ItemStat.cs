namespace GuildWars2.Items.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record ItemStat
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<ItemStatAttribute> Attributes { get; init; }
}
