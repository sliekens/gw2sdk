using GuildWars2.Hero.Races;
using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an item skin.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skin
{
    /// <summary>The skin ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the skin.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the skin as it appears in the tooltip.</summary>
    public required string Description { get; init; }

    /// <summary>Contains various modifiers.</summary>
    public required SkinFlags Flags { get; init; }

    /// <summary>The races required to use the skin.</summary>
    public required IReadOnlyList<RaceName> Races { get; init; }

    /// <summary>The item rarity of the skin.</summary>
    public required Rarity Rarity { get; init; }

    /// <summary>The URL of the skin icon.</summary>
    public required string? IconHref { get; init; }
}
