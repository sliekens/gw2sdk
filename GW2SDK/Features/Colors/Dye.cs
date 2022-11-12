using System;
using System.Collections.Generic;
using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Colors;

[PublicAPI]
[DataTransferObject]
public sealed record Dye
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required Color BaseRgb { get; init; }

    public required ColorInfo Cloth { get; init; }

    public required ColorInfo Leather { get; init; }

    public required ColorInfo Metal { get; init; }

    public required ColorInfo? Fur { get; init; }

    public required int? Item { get; init; }

    public required IReadOnlyCollection<ColorCategoryName> Categories { get; init; }
}
