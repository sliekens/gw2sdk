namespace GuildWars2.Items.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record AttributeCombination
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<Attribute> Attributes { get; init; }
}
