using System.Drawing;
using JetBrains.Annotations;

namespace GW2SDK.Maps;

[PublicAPI]
public sealed record ContinentRectangle
{
    public PointF TopLeft { get; init; }

    public SizeF Size { get; init; }
}
