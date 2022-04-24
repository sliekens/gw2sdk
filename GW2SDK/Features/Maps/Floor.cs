using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Maps;

[PublicAPI]
[DataTransferObject]
public sealed record Floor
{
    public int Id { get; init; }

    public SizeF TextureDimensions { get; init; }

    public ContinentRectangle? ClampedView { get; init; }

    public Dictionary<int, WorldRegion> Regions { get; init; } = new(0);
}
