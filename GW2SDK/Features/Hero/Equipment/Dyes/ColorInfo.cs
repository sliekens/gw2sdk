using System.Drawing;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>Information about how a dye color looks when applied to a material.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record ColorInfo
{
    /// <summary>The brightness of the color.</summary>
    public required int Brightness { get; init; }

    /// <summary>The contrast of the color.</summary>
    public required double Contrast { get; init; }

    /// <summary>The hue in the HSL colorspace.</summary>
    public required int Hue { get; init; }

    /// The saturation in the HSL colorspace.
    public required double Saturation { get; init; }

    /// The lightness in the HSL colorspace.
    public required double Lightness { get; init; }

    /// The precalculated RGB value.
    public required Color Rgb { get; init; }
}
