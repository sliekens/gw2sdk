namespace GuildWars2.Hero.Outfits;

[PublicAPI]
[DataTransferObject]
public sealed record Outfit
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Icon { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }
}
