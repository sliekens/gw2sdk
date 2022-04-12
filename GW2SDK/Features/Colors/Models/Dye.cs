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
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public Color BaseRgb { get; init; }

    public ColorInfo Cloth { get; init; } = new();

    public ColorInfo Leather { get; init; } = new();

    public ColorInfo Metal { get; init; } = new();

    public ColorInfo? Fur { get; init; }

    public int? Item { get; init; }

    public IReadOnlyCollection<ColorCategoryName> Categories { get; init; } = Array.Empty<ColorCategoryName>();
}