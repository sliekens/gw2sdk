using System.Drawing;

using JetBrains.Annotations;

namespace GW2SDK.Maps;

[PublicAPI]
public sealed record MapRectangle
{
    public PointF BottomLeft { get; init; }

    public SizeF Size { get; init; }
}