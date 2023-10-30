namespace GuildWars2.Banking;

[PublicAPI]
public sealed record MaterialCategory
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyList<int> Items { get; init; }

    public required int Order { get; init; }
}
