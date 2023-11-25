namespace GuildWars2.Hero.Equipment.Wardrobe;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required IReadOnlyCollection<SkinFlag> Flags { get; init; }

    public required IReadOnlyCollection<SkinRestriction> Restrictions { get; init; }

    public required Rarity Rarity { get; init; }

    public required string? IconHref { get; init; }
}
