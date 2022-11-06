using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Novelties;

[PublicAPI]
[DataTransferObject]
public sealed record Novelty
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public string Icon { get; init; } = "";

    public NoveltyKind Slot { get; init; }

    public IReadOnlyCollection<int> UnlockItems { get; init; } = Array.Empty<int>();
}
