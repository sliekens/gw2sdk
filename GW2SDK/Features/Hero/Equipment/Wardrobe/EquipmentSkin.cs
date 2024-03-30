using GuildWars2.Chat;
using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an equipment skin.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record EquipmentSkin
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
    public required Extensible<Rarity> Rarity { get; init; }

    /// <summary>The URL of the skin icon.</summary>
    public required string? IconHref { get; init; }

    /// <summary>Gets a chat link object for this skin.</summary>
    /// <returns>The chat link as an object.</returns>
    public SkinLink GetChatLink() => new() { SkinId = Id };
}
