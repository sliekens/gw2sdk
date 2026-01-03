using System.Text.Json.Serialization;

using GuildWars2.Chat;
using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an equipment skin.</summary>
[Inheritable]
[DataTransferObject]
[JsonConverter(typeof(EquipmentSkinJsonConverter))]
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
    public required IReadOnlyList<Extensible<RaceName>> Races { get; init; }

    /// <summary>The item rarity of the skin.</summary>
    public required Extensible<Rarity> Rarity { get; init; }

    /// <summary>The URL of the skin icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string? IconHref { get; init; }

    /// <summary>The URL of the skin icon.</summary>
    public required Uri? IconUrl { get; init; }

#pragma warning disable CA1024 // Use properties where appropriate
    /// <summary>Gets a chat link object for this skin.</summary>
    /// <returns>The chat link as an object.</returns>
    public SkinLink GetChatLink()
    {
        return new() { SkinId = Id };
    }
#pragma warning restore CA1024 // Use properties where appropriate
}
