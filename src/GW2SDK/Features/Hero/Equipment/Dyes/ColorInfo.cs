using System.Drawing;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>Information about how a dye color looks when applied to a material.</summary>
[DataTransferObject]
[JsonConverter(typeof(ColorInfoJsonConverter))]
public sealed record ColorInfo
{
    /// <summary>The brightness of the color.</summary>
    public required int Brightness { get; init; }

    /// <summary>The contrast of the color.</summary>
    public required double Contrast { get; init; }

    /// <summary>The hue in the HSL colorspace.</summary>
    public required int Hue { get; init; }

    /// <summary>The saturation in the HSL colorspace.</summary>
    public required double Saturation { get; init; }

    /// <summary>The lightness in the HSL colorspace.</summary>
    public required double Lightness { get; init; }

    /// <summary>The precalculated RGB value.</summary>
    public required Color Rgb { get; init; }
}
