using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required SkinFlags Flags { get; init; }

    public required IReadOnlyCollection<SkinRestriction> Restrictions { get; init; }

    public required Rarity Rarity { get; init; }

    /// <summary>The URL of the skin icon.</summary>
    public required string? IconHref { get; init; }
}
