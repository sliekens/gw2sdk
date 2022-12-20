using System.Collections.Generic;
using System.Drawing;
using GuildWars2.Annotations;
using JetBrains.Annotations;
using GeoRegion = GuildWars2.Exploration.Regions.Region;

namespace GuildWars2.Exploration.Floors;

[PublicAPI]
[DataTransferObject]
public sealed record Floor
{
    public required int Id { get; init; }

    public required SizeF TextureDimensions { get; init; }

    /// <summary>If present, it represents a rectangle of download-able textures. Every tile coordinate outside this rectangle
    /// is not available on the tile server.</summary>
    public required Area? ClampedView { get; init; }

    public required Dictionary<int, GeoRegion> Regions { get; init; }
}
