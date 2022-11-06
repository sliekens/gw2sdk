using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Outfits;

[PublicAPI]
[DataTransferObject]
public sealed record Outfit
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Icon { get; init; } = "";

    public IReadOnlyCollection<int> UnlockItems { get; init; } = Array.Empty<int>();
}
