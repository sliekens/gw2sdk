using System.Drawing;

namespace GuildWars2.Colors;

[PublicAPI]
[DataTransferObject]
public sealed record ColorInfo
{
    public required int Brightness { get; init; }

    public required double Contrast { get; init; }

    public required int Hue { get; init; }

    public required double Saturation { get; init; }

    public required double Lightness { get; init; }

    public required Color Rgb { get; init; }
}
