using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Colors;

[PublicAPI]
[DataTransferObject]
public sealed record ColorInfo
{
    public int Brightness { get; init; }

    public double Contrast { get; init; }

    public int Hue { get; init; }

    public double Saturation { get; init; }

    public double Lightness { get; init; }

    public Color Rgb { get; init; }
}
