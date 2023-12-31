using System.Drawing;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>Information about a dye color.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Dye
{
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
    public required Hue Hue { get; init; }

    /// <summary>The material category to which the dye belongs.</summary>
    public required Material Material { get; init; }

    /// <summary>The set to which the dye belongs.</summary>
    public required ColorSet Set { get; init; }
}
