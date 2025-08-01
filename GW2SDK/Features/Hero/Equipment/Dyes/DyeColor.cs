﻿using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Chat;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>Information about a dye color.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(DyeColorJsonConverter))]
public sealed record DyeColor
{
    /// <summary>The color ID of Dye Remover.</summary>
    public const int DyeRemoverId = 1;

    /// <summary>The color ID.</summary>
    public required int Id { get; init; }

    /// <summary>The color name.</summary>
    public required string Name { get; init; }

    /// <summary>The base RGB color.</summary>
    public required Color BaseRgb { get; init; }

    /// <summary>The appearance of the dye on cloth armor.</summary>
    public required ColorInfo Cloth { get; init; }

    /// <summary>The appearance of the dye on leather armor.</summary>
    public required ColorInfo Leather { get; init; }

    /// <summary>The appearance of the dye on metal armor.</summary>
    public required ColorInfo Metal { get; init; }

    /// <summary>The appearance of the dye on fur armor.</summary>
    public required ColorInfo? Fur { get; init; }

    /// <summary>The ID of the dye item which unlocks this dye color, or <c>null</c> if the dye is unlocked by default.</summary>
    public required int? ItemId { get; init; }

    /// <summary>The color category to which the dye belongs.</summary>
    public required Extensible<Hue> Hue { get; init; }

    /// <summary>The material category to which the dye belongs.</summary>
    public required Extensible<Material> Material { get; init; }

    /// <summary>The set to which the dye belongs.</summary>
    public required Extensible<ColorSet> Set { get; init; }

    /// <summary>Gets a chat link object for this item.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink? GetChatLink()
    {
        return ItemId.HasValue ? new ItemLink { ItemId = ItemId.Value } : null;
    }
}
