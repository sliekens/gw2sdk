namespace GuildWars2.Hero.Wallet;

[PublicAPI]
[DataTransferObject]
public sealed record Currency
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required int Order { get; init; }

    public required string IconHref { get; init; }
}
